using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
	[field: SerializeField] public float Speed { get; set; }
	[field: SerializeField] public float RotationSpeed { get; set; }
	[field: SerializeField] public float Stamina { get; set; }

	[SerializeField] private NavMeshAgent agent;
	private Character followTarget;

	public void MoveTo(Vector3 position)
	{
		followTarget = null;
		agent.stoppingDistance = 0f;
		agent.SetDestination(position);
	}

	public void Follow(Character character)
	{
		followTarget = character;
	}

	private void Update()
	{
		if (followTarget)
		{
			agent.stoppingDistance = 1.5f;
			var direction = (followTarget.transform.position - transform.position).normalized;
			agent.SetDestination(followTarget.transform.position - direction);
		}
	}

	private void Start()
	{
		RandomizeStats();
	}

	private void RandomizeStats()
	{
		Speed = Random.Range(1, 6);
		RotationSpeed = 30f * Random.Range(4, 13);
		Stamina = Random.Range(5, 11);
	}
}