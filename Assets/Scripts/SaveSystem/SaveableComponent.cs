using System.Collections.Generic;
using UnityEngine;
using MicKami.PolymorphicSerialization;
using System;
using MicKami.PolymorphicSerialization;
using UnityEngine;
using UnityEngine.Serialization;

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
}
