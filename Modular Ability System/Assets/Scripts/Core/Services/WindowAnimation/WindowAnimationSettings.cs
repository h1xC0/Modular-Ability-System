using Core.Services.ResourceProvider;
using UnityEngine;

namespace Core.Services.WindowAnimation
{
    [CreateAssetMenu(fileName = "Window Animation Settings", menuName = "Configurations/Window Animation Settings")]
    public class WindowAnimationSettings : ScriptableObject, IResource
    {
        [Header("Scale")]
        
        [SerializeField] private AnimationCurve _scaleCurve;
        public AnimationCurve ScaleCurve => _scaleCurve;
        
        [SerializeField] private float _scaleDuration;
        public float ScaleDuration => _scaleDuration;

        [Header("Fade")]
        
        [SerializeField] private AnimationCurve _fadeCurve;
        public AnimationCurve FadeCurve => _fadeCurve;

        [SerializeField] private float _fadeDuration;
        public float FadeDuration => _fadeDuration;
        
        [Header("Jump")]
        
        [SerializeField] private AnimationCurve _jumpCurve;
        public AnimationCurve JumpCurve => _jumpCurve;
        
        [SerializeField] private float _jumpDuration;
        public float JumpDuration => _jumpDuration;
        
        
        [Header("Sprite Change")]
        
        [SerializeField] private float _spriteChangeDuration;
        public float SpriteChangeDuration => _spriteChangeDuration;

        [Header("Size")] 
        
        [SerializeField] private AnimationCurve _sizeCurve;
        public AnimationCurve SizeCurve => _sizeCurve;
        
        [SerializeField] private float _sizeDuration;
        public float SizeDuration => _sizeDuration;
    }
}