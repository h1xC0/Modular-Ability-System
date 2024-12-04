using Shared.ExtensionTools;
using UnityEngine;

namespace Game.Combat.Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Configurations/Abilities/Create Ability")]
    public class AbilityConfiguration : TitledScriptableObject
    {
        public AnimationComponent AnimationComponent => _animationComponent;
        public AudioComponent AudioComponent => _audioComponent;
        public AreaAttackComponent AreaAttackComponent => _areaAttackComponent;    
        
        [SerializeField] private AnimationComponent _animationComponent;
        [SerializeField] private AudioComponent _audioComponent;
        [SerializeField] private AreaAttackComponent _areaAttackComponent;

        [SerializeReference] [HideInInspector] private VFXComponent _vfxComponent;
        [SerializeReference] [HideInInspector] public MovingComponent _movingComponent;
        [SerializeReference] [HideInInspector] private PropertiesModifierComponent _propertiesModifierComponent;

        protected override void OnValidate()
        {
            base.OnValidate();
            _vfxComponent = new VFXComponent();
            _movingComponent = new MovingComponent();
            _propertiesModifierComponent = new PropertiesModifierComponent();
        }
    }
}