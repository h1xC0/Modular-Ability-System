using Core.Services.CameraTransition.Configuration;
using Core.Services.ResourceProvider;
using UnityEditor;
using UnityEngine;

namespace Core.Services.CameraTransition.Editor
{
    public class CameraTransitionEditorWindow : EditorWindow
    {
        public CameraConfiguration CameraConfig;

        private IResourceProviderService _resourceProviderService;
        
        [MenuItem("CinemaCam/Camera Settings")]
        public static void ShowWindow()
        {
            CameraTransitionEditorWindow window = GetWindow<CameraTransitionEditorWindow>();
            window.titleContent = new GUIContent("CinemaCam Window");
        }

        private void CreateGUI()
        {
            _resourceProviderService = new ResourceProviderService();
            CameraConfig = _resourceProviderService.LoadResource<CameraConfiguration>();
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Profile");
            CameraConfig = EditorGUILayout.ObjectField(CameraConfig, typeof(CameraConfiguration), false) as CameraConfiguration;
            GUILayout.EndHorizontal();
        }
    }
}