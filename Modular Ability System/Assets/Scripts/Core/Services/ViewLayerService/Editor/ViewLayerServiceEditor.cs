using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Services.ViewLayerService.Editor
{
    [CustomEditor(typeof(ViewLayerService))]
    public class ViewLayerServiceEditor : UnityEditor.Editor
    {
        private static readonly Vector2 Resolution = new Vector2(1920, 1080);

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            
            var viewLayerService = (ViewLayerService)target;

            if (viewLayerService.HasValues)
            {
                if (GUILayout.Button("Clean Layers"))
                {
                    CleanLayers(viewLayerService);
                }
                return;
            }
            
            if (GUILayout.Button("Generate Layers"))
            {
                GenerateLayers(viewLayerService);
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private void GenerateLayers(ViewLayerService viewLayerService)
        {
            for (var index = 0; index < LayerName.Layers.Length; index++)
            {
                var layer = LayerName.Layers[index];

                GameObject layerObject = new GameObject(layer);
                var canvas = layerObject.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = index;

                var canvasScaler = layerObject.AddComponent<CanvasScaler>();
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                canvasScaler.matchWidthOrHeight = .5f;
                canvasScaler.referenceResolution = Resolution;

                var graphicRaycaster = layerObject.AddComponent<GraphicRaycaster>();
                graphicRaycaster.ignoreReversedGraphics = true;
                graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
                graphicRaycaster.blockingMask = ~0;

                var monoViewLayer = layerObject.AddComponent<MonoViewLayer>();
                monoViewLayer.LayerType = layer;
                monoViewLayer.IsChildOfPreviousLayer = index > 0;

                monoViewLayer.transform.SetParent(viewLayerService.transform);
                viewLayerService.AddLayer(monoViewLayer);

                PrefabUtility.RecordPrefabInstancePropertyModifications(monoViewLayer);
                EditorSceneManager.MarkSceneDirty(layerObject.scene);
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void CleanLayers(ViewLayerService viewLayerService)
        {
            while (viewLayerService.transform.childCount > 0)
            {
                var serviceChild = viewLayerService.transform.GetChild(0);

                if (Application.isPlaying)
                    Destroy(serviceChild.gameObject);
                else
                    DestroyImmediate(serviceChild.gameObject);
            }
            viewLayerService.ClearLayers();
        }
    }
}