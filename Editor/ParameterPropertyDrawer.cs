using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CustomPropertyDrawer(typeof(FloatParameter))]
public class FloatParameterDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels

	    using (new EditorGUILayout.HorizontalScope())
	    {
	        // Override checkbox
            DrawOverrideCheckbox(property.FindPropertyRelative("m_OverrideState"));

	        // Property
            using (new EditorGUI.DisabledScope(!property.FindPropertyRelative("m_OverrideState").boolValue))
	        {
                EditorGUI.indentLevel--;
	            // Default unity field
                EditorGUILayout.PropertyField(property.FindPropertyRelative("m_Value"), label);
                EditorGUI.indentLevel++;
	        }
	    }

        EditorGUI.EndProperty();
    }

	protected void DrawOverrideCheckbox(SerializedProperty property)
	{
	    var overrideRect = GUILayoutUtility.GetRect(17f, 17f, GUILayout.ExpandWidth(false));
	    overrideRect.yMin += 4f;
        property.boolValue = GUI.Toggle(overrideRect, property.boolValue, EditorGUIUtility.TrTextContent("", "Override this setting for this clip."), new GUIStyle("ShurikenToggle"));
	}
}