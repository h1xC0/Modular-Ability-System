using UnityEditor;
using UnityEngine;

namespace Game.Combat.Abilities.Editor
{
    [CustomEditor(typeof(AbilityConfiguration))]
    public class AbilityConfigurationEditor : UnityEditor.Editor
    {
        private SerializedProperty _vfxComponentProperty;
        private SerializedProperty _movingComponentProperty;
        private SerializedProperty _propertiesModifierComponentProperty;

        private bool _showVFXComponent;
        private bool _showMovingComponent;
        private bool _showPropertiesModifierComponent;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            
            _vfxComponentProperty = serializedObject.FindProperty("_vfxComponent");
            _movingComponentProperty = serializedObject.FindProperty("_movingComponent");
            _propertiesModifierComponentProperty = serializedObject.FindProperty("_propertiesModifierComponent");
            
            var abilityConfiguration = (AbilityConfiguration)target;
            if (_showVFXComponent)
            {
                EditorGUILayout.PropertyField(_vfxComponentProperty, true);
            }

            if (_showMovingComponent)
            {
                EditorGUILayout.PropertyField(_movingComponentProperty, true);
            }
            
            if (_showPropertiesModifierComponent)
            {
                EditorGUILayout.PropertyField(_propertiesModifierComponentProperty, true);
            }
            EditorGUILayout.Space(10);

            SetButtonComponent("VFX Component", !_showVFXComponent, ref _showVFXComponent);
            SetButtonComponent("Moving Component", !_showMovingComponent, ref _showMovingComponent);
            SetButtonComponent("Stats Modifier Component", !_showPropertiesModifierComponent, ref _showPropertiesModifierComponent);
            
            PrefabUtility.RecordPrefabInstancePropertyModifications(abilityConfiguration);
            serializedObject.ApplyModifiedProperties();
        }

        private void SetButtonComponent(string componentName, bool flag, ref bool componentFlag)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();

            string setTitle = componentFlag ? "Remove" : "Add";
            if (GUILayout.Button($"{setTitle} {componentName}",GUILayout.Width(250), GUILayout.Height(30)))
            {
                componentFlag = flag;
            }
                
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}