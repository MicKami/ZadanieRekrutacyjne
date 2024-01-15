using System;
using UnityEngine;

public class SaveCharacterAttributes : ISaveable
{
	[SerializeField] private Character character;

	public Type Type => typeof(SaveData);

	public void RestoreState(object obj)
	{
		var data = (SaveData)obj;
		character.Speed = data.speed;
		character.RotationSpeed = data.rotationSpeed;
		character.Stamina = data.stamina;
	}

	public object CaptureState()
	{
		return new SaveData()
		{
			speed = character.Speed,
			rotationSpeed = character.RotationSpeed,
			stamina = character.Stamina
		};
	}

	[Serializable]
	private struct SaveData
	{
		public float speed;
		public float rotationSpeed;
		public float stamina;
	}
}
