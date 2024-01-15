using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	private string path => Path.Combine(Application.persistentDataPath, "savegame.json");

	[field: SerializeField]
	public List<SaveableComponent> SavedComponents { get; set; }

	private Dictionary<Guid, Dictionary<long, object>> GetSceneState()
	{
		Dictionary<Guid, Dictionary<long, object>> result = new();
		foreach (var saveableComponent in SavedComponents)
		{
			result.Add(saveableComponent.Id, saveableComponent.CaptureState());
		}
		return result;
	}

	private void RestoreSceneState(Dictionary<Guid, Dictionary<long, object>> data)
	{
		foreach (var saveableComponent in SavedComponents)
		{
			if(data.TryGetValue(saveableComponent.Id, out var saveableState))
			{
				saveableComponent.RestoreState(saveableState);
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

	public void Save()
	{
		_ = SaveAsync();
	}

	public void Load()
	{
		_ = LoadAsync();
	}
}