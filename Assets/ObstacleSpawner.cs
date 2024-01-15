using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
	private const int OBSTACLE_COUNT = 6;
	[SerializeField] private GameObject obstaclePrefab;
	public List<GameObject> SpawnedObstacles { get; private set; }

	private void Start()
	{
		SpawnedObstacles = new();
		for (int i = 0; i < OBSTACLE_COUNT; i++)
		{
			var spawn = Instantiate(obstaclePrefab);

			spawn.transform.position = new Vector3(Random.Range(-15f, 20f), 0.5f, Random.Range(-15f, 20f));
			spawn.transform.localScale = new Vector3(Random.Range(4, 16), 1, 1);
			spawn.transform.rotation = Quaternion.Euler(new Vector3(0, 90f * Random.Range(0, 2), 0));

			SpawnedObstacles.Add(spawn);
		}
	}
}
