using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
	[field: SerializeField] public float Speed { get; set; }
	[field: SerializeField] public float RotationSpeed { get; set; }
	[field: SerializeField] public float Stamina { get; set; }

	[SerializeField] private NavMeshAgent agent;
	public Character FollowTarget { get; private set; }
	public Vector3 FollowPosition { get; private set; }
	public bool HasTarget { get; private set; } = false;

	public void MoveTo(Vector3 position)
	{
		HasTarget = true;
		FollowPosition = position;
		FollowTarget = null;
		agent.stoppingDistance = 0f;
		agent.SetDestination(FollowPosition);
	}

	public void Follow(Character character)
	{
		HasTarget = true;
		FollowTarget = character;
	}

	private void Update()
	{
		if (FollowTarget)
		{
			agent.stoppingDistance = 1.5f;
			var direction = (FollowTarget.transform.position - transform.position).normalized;
			agent.SetDestination(FollowTarget.transform.position - direction);
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