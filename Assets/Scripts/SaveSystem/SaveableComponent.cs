using System.Collections.Generic;
using UnityEngine;
using MicKami.PolymorphicSerialization;

[RequireComponent(typeof(SerializedGUIDComponent))]
public class SaveableComponent : MonoBehaviour
{
    [SerializeReference, Polymorphic]
    private List<ISaveable> saveables = new();
}
