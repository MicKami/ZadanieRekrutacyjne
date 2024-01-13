using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SerializedGUIDComponent))]
public class GuidComponentDrawer : Editor
{
	private SerializedGUIDComponent guidComp;

	public override void OnInspectorGUI()
	{
		if (guidComp == null)
		{
			guidComp = (SerializedGUIDComponent)target;
		}

		EditorGUI.BeginDisabledGroup(true);
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("ID: ", GUILayout.Width(EditorStyles.label.CalcSize(new GUIContent("ID: ")).x));
		EditorGUILayout.TextField(guidComp.Id.ToString());
		EditorGUILayout.EndHorizontal();
		EditorGUI.EndDisabledGroup();
	}
}