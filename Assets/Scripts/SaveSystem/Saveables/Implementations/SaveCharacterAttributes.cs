using System;
using UnityEngine;

public class SaveCharacterAttributes : ISaveable
{
	[SerializeField] private Character characterToSave;

	public Type Type => typeof(CharacterData);

	public void RestoreState(object obj)
	{
		var data = (CharacterData)obj;
		characterToSave.Speed = data.speed;
		characterToSave.RotationSpeed = data.rotationSpeed;
		characterToSave.Stamina = data.stamina;
	}

	public object CaptureState()
	{
		return new CharacterData()
		{
			speed = characterToSave.Speed,
			rotationSpeed = characterToSave.RotationSpeed,
			stamina = characterToSave.Stamina
		};
	}

	[Serializable]
	private struct CharacterData
	{
		public float speed;
		public float rotationSpeed;
		public float stamina;
	}

}
