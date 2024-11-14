using UnityEngine;

namespace Core.Services.ViewLayerService
{
    [RequireComponent(typeof(Canvas))]
    public class MonoViewLayer : MonoBehaviour
    {
        public string LayerType;
        public bool IsChildOfPreviousLayer;
        public Canvas Canvas => GetComponent<Canvas>();
    }
}