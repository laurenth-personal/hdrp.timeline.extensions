using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class ParameterEditorUtilities
{
    public static bool ParameterField(SerializedProperty property, GUIContent label)
    {
        bool value = false;
        using (new EditorGUILayout.HorizontalScope())
        {
            // Override checkbox
            DrawOverrideCheckbox(property.FindPropertyRelative("m_OverrideState"));

            // Property
            using (new EditorGUI.DisabledScope(!property.FindPropertyRelative("m_OverrideState").boolValue))
            {
                EditorGUI.indentLevel--;
                // Default unity field
                value = EditorGUILayout.PropertyField(property.FindPropertyRelative("m_Value"), label);
                EditorGUI.indentLevel++;
            }
        }
        return value;
    }

    public static float SliderParameterField(SerializedProperty property, GUIContent label)
    {
        float value = 0;
        using (new EditorGUILayout.HorizontalScope())
        {
            // Override checkbox
            DrawOverrideCheckbox(property.FindPropertyRelative("m_OverrideState"));

            // Property
            using (new EditorGUI.DisabledScope(!property.FindPropertyRelative("m_OverrideState").boolValue))
            {
                EditorGUI.indentLevel--;
                // Default unity field
                property.FindPropertyRelative("m_Value").floatValue = EditorGUILayout.Slider(label, property.FindPropertyRelative("m_Value").floatValue, property.FindPropertyRelative("min").floatValue, property.FindPropertyRelative("max").floatValue);
                EditorGUI.indentLevel++;
            }
        }
        return value;
    }

    protected static void DrawOverrideCheckbox(SerializedProperty property)
	{
	    var overrideRect = GUILayoutUtility.GetRect(17f, 17f, GUILayout.ExpandWidth(false));
	    overrideRect.yMin += 4f;
        property.boolValue = GUI.Toggle(overrideRect, property.boolValue, EditorGUIUtility.TrTextContent("", "Override this setting for this clip."), new GUIStyle("ShurikenToggle"));
	}
}