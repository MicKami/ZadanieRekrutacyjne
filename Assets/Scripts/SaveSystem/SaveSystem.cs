using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	private string path => Path.Combine(Application.persistentDataPath, "savegame.json");

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

	public async Task SaveAsync()
	{
		var json = JsonConvert.SerializeObject(GetSceneState(), Formatting.Indented);
		await File.WriteAllTextAsync(path, json, destroyCancellationToken);
	}
	public async Task LoadAsync()
	{
		var json = await File.ReadAllTextAsync(path, destroyCancellationToken);
		var data = JsonConvert.DeserializeObject<Dictionary<Guid, Dictionary<long, object>>>(json);
		RestoreSceneState(data);
	}
}