using System.Collections.Generic;
using UnityEngine;

public class SaveableComponent : MonoBehaviour
{
    [SerializeReference]
    private List<ISaveable> saveables = new();
}
