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
			characterSelectionData.Selected = isOn ? character : null;
		});
	}

	private void Update()
	{
		characterSpeedText.text = $"Speed: {character.Speed}";
		characterAgilityText.text = $"Agility: {character.RotationSpeed}";
		characterStaminaText.text = $"Stamina: {character.Stamina}";
	}
	private void OnSelectionChange()
	{
		if (characterSelectionData.Selected == character)
		{
			button.targetGraphic.color = selectedColor;
			button.isOn = true;
		}
		else
		{
			button.targetGraphic.color = Color.white;
			button.isOn = false;
		}
	}

	private void OnEnable()
	{
		characterSelectionData.OnSelectionChange += OnSelectionChange;
	}


	private void OnDisable()
	{
		characterSelectionData.OnSelectionChange -= OnSelectionChange;

	}
}
