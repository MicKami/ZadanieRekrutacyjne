using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SaveCharacterSelection : ISaveable
{
	[SerializeField] private CharacterSelectionData selectionData;

	public Type Type => typeof(SaveData);

	public void RestoreState(object obj)
	{
		var data = (SaveData)obj;
		selectionData.Selected = selectionData.AllCharacters.Find(c => c.GetComponent<SerializedGUIDComponent>().Id == data.selectedCharacterId);
	}

	public object CaptureState()
	{
		return new SaveData()
		{
			selectedCharacterId = selectionData.Selected ? selectionData.Selected.GetComponent<SerializedGUIDComponent>().Id : Guid.Empty
		};
	}

	[Serializable]
	private struct SaveData
	{
		public Guid selectedCharacterId;
	}
}