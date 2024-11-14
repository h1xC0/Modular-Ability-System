using Core.Services.ResourceProvider;
using UnityEngine;

namespace Shared.Constants
{
    [CreateAssetMenu(fileName = "LayerMask Configuration", menuName = "Configurations/LayerMask Configurations")]
    public class LayerMaskConfiguration : ScriptableObject, IResource
    {
        // [SerializeField] private LayerMask _placementLayerMask;
        // public LayerMask PlacementLayerMask => _placementLayerMask;
        [Header("LayerMasks")]
        [SerializeField] private LayerMask _interactLayerMask;
        public LayerMask InteractLayerMask => _interactLayerMask;
        
        [SerializeField] private LayerMask _ignoreRaycastLayerMask;
        public LayerMask IgnoreRaycastLayerMask => _ignoreRaycastLayerMask;
        
        [SerializeField] private LayerMask _playerLayerMask;
        public LayerMask PlayerLayerMask => _playerLayerMask;

        [SerializeField] private LayerMask _npcLayerMask;
        public LayerMask NPCLayerMask => _npcLayerMask;

        [SerializeField] private LayerMask _groundLayerMask;
        public LayerMask GroundLayerMask => _groundLayerMask;

        
        [Header("Tags")]
        [SerializeField] private string _potTag;
        public string PotTag => _potTag;
        
        [SerializeField] private string _dirtPileTag;
        public string DirtPileTag => _dirtPileTag;
        
        public int GetLayerIndex(LayerMask selectedLayer) =>
            Mathf.RoundToInt(Mathf.Log(selectedLayer.value, 2));
    }
}