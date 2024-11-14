using System;
using Shared.Enums;
using Shared.ExtensionTools.Attributes.ReadOnly;
using UnityEditor;
using UnityEngine;

namespace Shared.ExtensionTools.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var defaultColor = GUI.color;

            var readOnlyAttribute = (ReadOnlyAttribute)attribute;

            GUI.color = readOnlyAttribute.GUIColor switch
            {
                GUIColor.Standard => Color.gray,
                GUIColor.Green => Color.green,
                GUIColor.Yellow => Color.yellow,
                GUIColor.Red => Color.red,
                _ => GUI.color
            };

            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
            GUI.color = defaultColor;
        }
    }
}