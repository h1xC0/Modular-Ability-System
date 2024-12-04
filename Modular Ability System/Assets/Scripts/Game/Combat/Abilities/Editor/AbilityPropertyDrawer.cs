using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Combat.Abilities.Editor
{
    // [CustomPropertyDrawer(typeof(IAbilityComponent))]
    public class AbilityPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            // Create property container element.
            var container = new VisualElement();

            // Create property fields.
            var lookAtTarget = new PropertyField(property.FindPropertyRelative("_lookAtTarget"));
            var moveForward = new PropertyField(property.FindPropertyRelative("_moveForward"));
            var moveStep = new PropertyField(property.FindPropertyRelative("_moveStep"));

            // Add fields to the container.
            container.Add(lookAtTarget);
            container.Add(moveForward);
            container.Add(moveStep);

            return container;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var lookAtTargetRect = new Rect(position.x, position.y, 30, position.height);
            var moveForwardRect = new Rect(position.x + 35, position.y, 50, position.height);
            var moveStepRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

            // Draw fields - pass GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(lookAtTargetRect, property.FindPropertyRelative("_lookAtTarget"), GUIContent.none);
            EditorGUI.PropertyField(moveForwardRect, property.FindPropertyRelative("_moveForward"), GUIContent.none);
            EditorGUI.PropertyField(moveStepRect, property.FindPropertyRelative("_moveStep"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}