using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour
{
	[SerializeField] private CharacterSelectionData selectionData;

	private void Update()
	{
		if (selectionData.Selected == null) return;

		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			if (TryGetFloorPoint(out var point))
			{
				foreach (var character in selectionData.AllCharacters)
				{
					if (selectionData.Selected == character)
					{
						character.MoveTo(point);
					}
					else
					{
						character.Follow(selectionData.Selected);
					}
				} 
			}
		}
	}

	private bool TryGetFloorPoint(out Vector3 point)
	{
		point = Vector3.zero;
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (plane.Raycast(ray, out var distance))
		{
			point = ray.GetPoint(distance);
			return true;
		}
		return false;
	}
}
