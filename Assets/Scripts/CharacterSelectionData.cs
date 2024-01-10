using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New " + nameof(CharacterSelectionData), menuName = nameof(CharacterSelectionData))]
public class CharacterSelectionData : ScriptableObject
{
	public List<Character> AllCharacters { get; set; } = new();
	public Character Selected { get; set; }
}
