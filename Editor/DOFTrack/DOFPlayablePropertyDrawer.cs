using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DOFPlayable))]
public class DOFPlayableDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUILayout.PropertyField(property.FindPropertyRelative("overrideDOFMode"));

        EditorGUILayout.PropertyField(property.FindPropertyRelative("depthOfFieldMode"));

        EditorGUI.BeginDisabledGroup(property.FindPropertyRelative("depthOfFieldMode").enumValueIndex != 1);
        EditorGUILayout.PropertyField(property.FindPropertyRelative("overrideFocusDistance"));
        EditorGUI.BeginDisabledGroup(!property.FindPropertyRelative("overrideFocusDistance").boolValue);
        EditorGUILayout.PropertyField(property.FindPropertyRelative("focusDistance"));
        EditorGUI.EndDisabledGroup();
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(new GUIContent("Near blur"), EditorStyles.boldLabel);
        EditorGUI.BeginDisabledGroup(property.FindPropertyRelative("depthOfFieldMode").enumValueIndex != 2);
        EditorGUILayout.PropertyField(property.FindPropertyRelative("overrideNearStart"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("nearStart"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("overrideNearEnd"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("focusDistance"), new GUIContent("nearEnd"));
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.PropertyField(property.FindPropertyRelative("overrideNearMaxRadius"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("nearMaxRadius"));

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(new GUIContent("Far blur"), EditorStyles.boldLabel);
        EditorGUI.BeginDisabledGroup(property.FindPropertyRelative("depthOfFieldMode").enumValueIndex != 2);
        EditorGUILayout.PropertyField(property.FindPropertyRelative("overrideFarStart"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("farStart"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("overrideFarEnd"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("farEnd"));
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.PropertyField(property.FindPropertyRelative("overrideFarMaxRadius"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("farMaxRadius"));

        EditorGUI.EndProperty();
    }
}