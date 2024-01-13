using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	private Dictionary<Guid, SaveableComponent> GetIdToSaveablesMap()
	{
		Dictionary<Guid, SaveableComponent> result = new();
		foreach (var saveableComponent in FindObjectsOfType<SaveableComponent>())
		{
			result.Add(saveableComponent.Id, saveableComponent);
		}
		return result;
	}

	private Dictionary<Guid, Dictionary<long, object>> GetSceneState()
	{
		Dictionary<Guid, Dictionary<long, object>> result = new();
		foreach (var saveableComponent in FindObjectsOfType<SaveableComponent>())
		{
			result.Add(saveableComponent.Id, saveableComponent.CaptureState());
		}
		return result;
	}

	private void RestoreSceneState(Dictionary<Guid, Dictionary<long, object>> data)
	{
		var idToSaveable = GetIdToSaveablesMap();
		foreach (var kvp in data)
		{
			if (idToSaveable.TryGetValue(kvp.Key, out var saveableComponent))
			{
				saveableComponent.RestoreState(kvp.Value);
			}
		}
	}
}