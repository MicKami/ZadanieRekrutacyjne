/*
Modified version of:
https://github.com/Unity-Technologies/guid-based-reference

Guid based Reference copyright � 2018 Unity Technologies ApS
Licensed under the Unity Companion License for Unity-dependent projects--see Unity Companion License.
Unless expressly provided otherwise, the Software under this license is made available strictly on an �AS IS� BASIS WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED.
Please review the license for details on these and other terms and conditions.
 */

using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

// This component gives a GameObject a stable, non-replicatable Globally Unique IDentifier.
[ExecuteInEditMode, DisallowMultipleComponent]
public class SerializedGUIDComponent : MonoBehaviour, ISerializationCallbackReceiver
{
	private static Dictionary<System.Guid, SerializedGUIDComponent> map = new();

	[SerializeField] private byte[] serializedGuid;
	private System.Guid guid = System.Guid.Empty;
	public System.Guid Id
	{
		get
		{
			if (!IsGuidAssigned() && serializedGuid != null && serializedGuid.Length == 16)
			{
				guid = new System.Guid(serializedGuid);
			}
			return guid;
		}
	}

	public bool IsGuidAssigned()
	{
		return guid != System.Guid.Empty;
	}

	// When de-serializing or creating this component, we want to either restore our serialized GUID
	// or create a new one.
	private void CreateGuid()
	{
		// if our serialized data is invalid, then we are a new object and need a new GUID
		if (serializedGuid == null || serializedGuid.Length != 16 && !IsGuidAssigned())
		{
#if UNITY_EDITOR
			// if in editor, make sure we aren't a prefab of some kind
			if (IsAssetOnDisk())
			{
				return;
			}
			Undo.RecordObject(this, "Added GUID");
#endif
			guid = System.Guid.NewGuid();
			serializedGuid = guid.ToByteArray();
#if UNITY_EDITOR
			// If we are creating a new GUID for a prefab instance of a prefab, but we have somehow lost our prefab connection
			// force a save of the modified prefab instance properties
			if (PrefabUtility.IsPartOfNonAssetPrefabInstance(this))
			{
				PrefabUtility.RecordPrefabInstancePropertyModifications(this);
			}
#endif
		}
		else if (!IsGuidAssigned())
		{
			// otherwise, we should set our system guid to our serialized guid
			guid = new System.Guid(serializedGuid);
		}

		// register with the GUID Manager so that other components can access this
		if (IsGuidAssigned())
		{
			if (map.ContainsKey(Id) && map[Id] != this)
			{
				// if registration fails, we probably have a duplicate or invalid GUID, get us a new one.
				serializedGuid = null;
				guid = System.Guid.Empty;
				CreateGuid();
			}
			else
			{
				map.TryAdd(Id, this);
			}
		}
	}

#if UNITY_EDITOR
	private bool IsEditingInPrefabMode()
	{
		if (EditorUtility.IsPersistent(this))
		{
			// if the game object is stored on disk, it is a prefab of some kind, despite not returning true for IsPartOfPrefabAsset =/
			return true;
		}
		else
		{
			// If the GameObject is not persistent let's determine which stage we are in first because getting Prefab info depends on it
			var mainStage = StageUtility.GetMainStageHandle();
			var currentStage = StageUtility.GetStageHandle(gameObject);
			if (currentStage != mainStage)
			{
				var prefabStage = PrefabStageUtility.GetPrefabStage(gameObject);
				if (prefabStage != null)
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool IsAssetOnDisk()
	{
		return IsEditingInPrefabMode() || PrefabUtility.IsPartOfPrefabAsset(this);
	}
#endif

	// We cannot allow a GUID to be saved into a prefab, and we need to convert to byte[]
	public void OnBeforeSerialize()
	{
#if UNITY_EDITOR
		// This lets us detect if we are a prefab instance or a prefab asset.
		// A prefab asset cannot contain a GUID since it would then be duplicated when instanced.
		if (IsAssetOnDisk())
		{
			serializedGuid = null;
			guid = System.Guid.Empty;
		}
		else
#endif
		{
			if (IsGuidAssigned())
			{
				serializedGuid = guid.ToByteArray();
			}
		}
	}

	// On load, we can go head a restore our system guid for later use
	public void OnAfterDeserialize()
	{
		if (serializedGuid != null && serializedGuid.Length == 16)
		{
			guid = new System.Guid(serializedGuid);
		}
	}

	private void Awake()
	{
		CreateGuid();
	}

	private void OnValidate()
	{
#if UNITY_EDITOR
		// similar to on Serialize, but gets called on Copying a Component or Applying a Prefab
		// at a time that lets us detect what we are
		if (IsAssetOnDisk())
		{
			serializedGuid = null;
			guid = System.Guid.Empty;
		}
		else
#endif
		{
			CreateGuid();
		}
	}

	private void OnDestroy()
	{
		map.Remove(Id);
	}
}