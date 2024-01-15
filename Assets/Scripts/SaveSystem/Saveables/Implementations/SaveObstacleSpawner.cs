using System;
using System.Collections.Generic;
using UnityEngine;
using static JsonSerializedTypes;

public class SaveObstacleSpawner : ISaveable
{
	[SerializeField] private ObstacleSpawner obstacleSpawner;

	public Type Type => typeof(SaveData);

	public void RestoreState(object obj)
	{
		var data = (SaveData)obj;
		for (int i = 0; i < obstacleSpawner.SpawnedObstacles.Count; i++)
		{
			obstacleSpawner.SpawnedObstacles[i].transform.position = data.obstacles[i].position;
			obstacleSpawner.SpawnedObstacles[i].transform.rotation = data.obstacles[i].rotation;
			obstacleSpawner.SpawnedObstacles[i].transform.localScale = data.obstacles[i].scale;
		}
	}

	public object CaptureState()
	{
		List<ObstacleData> state = new();
		foreach (var obstacle in obstacleSpawner.SpawnedObstacles)
		{
			state.Add(new ObstacleData()
			{
				position = obstacle.transform.position,
				rotation = obstacle.transform.rotation,
				scale = obstacle.transform.localScale
			});
		}
		return new SaveData()
		{
			obstacles = state
		};
	}

	[Serializable]
	private struct SaveData
	{
		public List<ObstacleData> obstacles;
	}
	[Serializable]
	private struct ObstacleData
	{
		public Vector3Json position;
		public Vector4Json rotation;
		public Vector3Json scale;
	}

}