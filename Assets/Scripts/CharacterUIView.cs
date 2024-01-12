using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIView : MonoBehaviour
{
	[SerializeField] private Character character;
	[SerializeField] private CharacterSelectionData characterSelectionData;
	[SerializeField] private Toggle button;
	[SerializeField] private TextMeshProUGUI characterNameText;
	[SerializeField] private TextMeshProUGUI characterSpeedText;
	[SerializeField] private TextMeshProUGUI characterAgilityText;
	[SerializeField] private TextMeshProUGUI characterStaminaText;
	[SerializeField] private Color selectedColor;
	private void Start()
	{
		characterNameText.text = character.gameObject.name;

		button.onValueChanged.AddListener((bool isOn) =>
		{
			button.targetGraphic.color = isOn ? selectedColor : Color.white;
			characterSelectionData.Selected = isOn ? character : null;
		});
	}

	private void Update()
	{
		characterSpeedText.text = $"Speed: {character.Speed}";
		characterAgilityText.text = $"Agility: {character.RotationSpeed}";
		characterStaminaText.text = $"Stamina: {character.Stamina}";
	}
}
