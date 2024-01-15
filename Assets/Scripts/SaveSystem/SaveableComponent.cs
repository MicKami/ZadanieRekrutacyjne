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

	private Dictionary<ISaveable, long> objectToIdMap;
	private void Awake()
	{
		objectToIdMap = new();
		foreach (var saveable in saveables)
		{
			long id = ManagedReferenceUtility.GetManagedReferenceIdForObject(this, saveable);
			objectToIdMap.TryAdd(saveable, id);
		}
	}

	public Dictionary<long, object> CaptureState()
	{
		Dictionary<long, object> result = new();
		foreach (var saveable in saveables)
		{
			if (objectToIdMap.TryGetValue(saveable, out var id))
			{
				result.Add(id, saveable.CaptureState());
			}
		}
		return result;
	}

	public void RestoreState(Dictionary<long, object> data)
	{
		foreach (var saveable in saveables)
		{
			if (objectToIdMap.TryGetValue(saveable, out var id))
			{
				if(data.TryGetValue(id, out var dataObject))
				{
					var state = ((JObject)dataObject).ToObject(saveable.Type);
					saveable.RestoreState(state);
				}	
			}
		}
	}
}
