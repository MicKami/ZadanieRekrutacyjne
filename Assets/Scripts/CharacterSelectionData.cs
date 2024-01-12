using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New " + nameof(CharacterSelectionData), menuName = nameof(CharacterSelectionData))]
public class CharacterSelectionData : ScriptableObject
{
	public List<Character> AllCharacters { get; set; } = new();
	public Character Selected { get; set; }

#if UNITY_EDITOR
	//Reset SO state after exiting playmode
	private void EditorApplication_playModeStateChanged(UnityEditor.PlayModeStateChange state)
	{
		if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
		{
			AllCharacters.Clear();
			Selected = null;
		}
	}
	private void OnEnable()
	{
		UnityEditor.EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
	}

	private void OnDisable()
	{
		UnityEditor.EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
	}

#endif

}
