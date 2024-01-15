using System;
using UnityEngine;
using static JsonSerializedTypes;

public class SaveCharacterMovementTargets : ISaveable
{
	[SerializeField] private Character character;
	[SerializeField] private CharacterSelectionData selectionData;

	public Type Type => typeof(SaveData);

	public void RestoreState(object obj)
	{
		var data = (SaveData)obj;

		if (data.followTargetGUID != Guid.Empty)
		{
			Character target = selectionData.AllCharacters.Find(c => c.GetComponent<SerializedGUIDComponent>().Id == data.followTargetGUID);
			character.Follow(target);
		}
		else if (data.hasTarget)
		{
			character.MoveTo(data.followPosition);
		}
	}

	public object CaptureState()
	{
		return new SaveData()
		{
			hasTarget = character.HasTarget,
			followPosition = character.FollowPosition,
			followTargetGUID = character.FollowTarget ? character.FollowTarget.GetComponent<SerializedGUIDComponent>().Id : Guid.Empty
		};
	}

	[Serializable]
	private struct SaveData
	{
		public Vector3Json followPosition;
		public Guid followTargetGUID;
		public bool hasTarget;
	}
}