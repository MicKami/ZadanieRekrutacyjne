using System.Collections.Generic;
using System;
using MicKami.PolymorphicSerialization;
using UnityEngine;
using UnityEngine.Serialization;
using Newtonsoft.Json.Linq;

[RequireComponent(typeof(SerializedGUIDComponent))]
public class SaveableComponent : MonoBehaviour
{
	[SerializeReference, Polymorphic]
	private List<ISaveable> saveables = new();

	private SerializedGUIDComponent guid;
	public Guid Id
	{
		get
		{
			guid ??= GetComponent<SerializedGUIDComponent>();
			return guid.Id;
		}
	}

	private Dictionary<long, ISaveable> idToObjectMap;
	private Dictionary<ISaveable, long> objectToIdMap;
	private void Awake()
	{
		idToObjectMap = new();
		objectToIdMap = new();
		foreach (var saveable in saveables)
		{
			long id = ManagedReferenceUtility.GetManagedReferenceIdForObject(this, saveable);
			idToObjectMap.TryAdd(id, saveable);
			objectToIdMap.TryAdd(saveable, id);
		}
	}

	public Dictionary<long, object> CaptureState()
	{
		Dictionary<long, object> result = new();
		foreach (var saveable in saveables)
		{
			if(objectToIdMap.TryGetValue(saveable, out var id))
			{ 
				result.Add(id, saveable.CaptureState());
			}
		}
		return result;
	}

	public void RestoreState(Dictionary<long, object> data)
	{
		foreach (var kvp in data)
		{
			long id = kvp.Key;
			if(idToObjectMap.TryGetValue(id, out var saveable))
			{
				object dataObject = data[id];
				var state = ((JObject)dataObject).ToObject(saveable.Type);
				saveable.RestoreState(state);
			}
		}
	}
}
