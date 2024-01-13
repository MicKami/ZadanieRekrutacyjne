using System.Collections.Generic;
using UnityEngine;
using MicKami.PolymorphicSerialization;
using System;

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

}
