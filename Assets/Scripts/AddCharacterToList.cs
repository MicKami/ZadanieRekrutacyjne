using UnityEngine;

public class AddCharacterToList : MonoBehaviour
{
	[SerializeField] private Character character;
	[SerializeField] private CharacterSelectionData list;

	private void OnEnable()
	{
		list.AllCharacters.Add(character);
	}

	private void OnDisable()
	{
		list.AllCharacters.Remove(character);
	}
}
