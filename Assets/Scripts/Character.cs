using UnityEngine;

public class Character : MonoBehaviour
{
	[field: SerializeField] public float Speed { get; set; }
	[field: SerializeField] public float RotationSpeed { get; set; }
	[field: SerializeField] public float Stamina { get; set; }

	public void MoveTo(Vector3 position)
	{

	}

	public void Follow(Character character)
	{

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