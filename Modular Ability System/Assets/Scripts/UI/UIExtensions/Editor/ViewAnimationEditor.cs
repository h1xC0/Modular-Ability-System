using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Shared.Enums.WindowAnimationType;

namespace UI.UIExtensions.Editor
{
    [CustomEditor(typeof(ViewAnimation))]
    public class ViewAnimationEditor : UnityEditor.Editor
    {
        private SerializedProperty _newSpriteProperty;
        private SerializedProperty _imageProperty;
        private SerializedProperty _canvasProperty;
        private SerializedProperty _offsetProperty;
        private SerializedProperty _sizeProperty;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            _newSpriteProperty = serializedObject.FindProperty("_newSprite");
            _imageProperty = serializedObject.FindProperty("_image");
            _canvasProperty = serializedObject.FindProperty("_canvasGroup");
            _offsetProperty = serializedObject.FindProperty("_offset");
            _sizeProperty = serializedObject.FindProperty("_originalSize");
            
            var viewAnimation = (ViewAnimation)target;
            if (viewAnimation.WindowAnimationType is Float or Jump)
            {
                _offsetProperty.vector3Value = EditorGUILayout.Vector3Field("Offset", _offsetProperty.vector3Value);
            }
            
            if (viewAnimation.WindowAnimationType is Fade or Float)
            {
                if(viewAnimation.Image == null)
                    EditorGUILayout.PropertyField(_canvasProperty);
                
                if(viewAnimation.CanvasGroup == null)
                    EditorGUILayout.PropertyField(_imageProperty);
                
                if (viewAnimation.CanvasGroup == null && viewAnimation.Image == null && Application.isPlaying)
                {
                    _canvasProperty.objectReferenceValue = viewAnimation.AddComponent<CanvasGroup>();
                }
            }
            
            if (viewAnimation.WindowAnimationType == SpriteChange)
            {
                if ((Image)_imageProperty.objectReferenceValue == null && Application.isPlaying)
                {
                    _imageProperty.objectReferenceValue = viewAnimation.AddComponent<Image>();
                }
                EditorGUILayout.PropertyField(_newSpriteProperty);
                EditorGUILayout.PropertyField(_imageProperty);
            }

            if (viewAnimation.WindowAnimationType == Size)
            {
                _sizeProperty.vector2Value = EditorGUILayout.Vector2Field("Original Size", _sizeProperty.vector2Value);
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button($"{viewAnimation.WindowAnimationType} In"))
            {
                viewAnimation
                    .ViewSequence(ViewAnimation.In)
                    .Play();
            }

            if (GUILayout.Button($"{viewAnimation.WindowAnimationType} Out"))
            {
                viewAnimation
                    .ViewSequence(ViewAnimation.Out)
                    .Play();
            }
            GUILayout.EndHorizontal();

            PrefabUtility.RecordPrefabInstancePropertyModifications(viewAnimation);
            serializedObject.ApplyModifiedProperties();
        }
    }
}