using Core.Services.ResourceProvider;
using DG.Tweening;
using Shared.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Services.WindowAnimation
{
    public class WindowAnimationService : IWindowAnimationService
    {
        private readonly WindowAnimationSettings _windowAnimationSettings;
        
        public WindowAnimationService(IResourceProviderService resourceProviderService)
        {
            _windowAnimationSettings = resourceProviderService.LoadResource<WindowAnimationSettings>();
        }
        
        public Sequence FadeInOut(Image target, float alpha)
        {
            var sequence = DOTween.Sequence(target);
            sequence.SetAutoKill();

            sequence
                .AppendCallback(() =>
                {
                    target.DOFade(alpha == 0 ? 1 : 0, 0f);
                    var targetInteractable = alpha == 0;
                    target.raycastTarget = targetInteractable;
                })
                .AppendInterval(AnimationConstants.OneTenth)
                .Append(target
                    .DOFade(alpha, _windowAnimationSettings.FadeDuration)
                    .OnComplete(() =>
                    {
                        var targetInteractable = alpha != 0;
                        target.raycastTarget = targetInteractable;
                    }))
                .SetEase(_windowAnimationSettings.FadeCurve);
            return sequence;
        }
        
        public Sequence FadeInOut(CanvasGroup target, float alpha)
        {
            var sequence = DOTween.Sequence(target);
            sequence.SetAutoKill();

            sequence.AppendCallback(() =>
                {
                    target.alpha = alpha == 0 ? 1 : 0;
                    var targetInteractable = alpha == 0;
                    target.interactable = target.blocksRaycasts = targetInteractable;
                })
                .AppendInterval(AnimationConstants.OneTenth)
                .Append(target
                    .DOFade(alpha, _windowAnimationSettings.FadeDuration)
                    .OnComplete(() =>
                    {
                        var targetInteractable = alpha != 0;
                        target.interactable = target.blocksRaycasts = targetInteractable;
                    }))
                .SetEase(_windowAnimationSettings.FadeCurve);
            return sequence;
        }

        public Sequence ScaleInOut(Transform target, float scale)
        {
            var sequence = DOTween.Sequence(target);
            sequence.SetAutoKill();

            sequence
                .AppendCallback(() => target.localScale = scale == 0 ? Vector3.one : Vector3.zero)
                .AppendInterval(AnimationConstants.OneTenth)
                .Append(target.DOScale(scale, _windowAnimationSettings.ScaleDuration))
                .SetEase(_windowAnimationSettings.ScaleCurve);
            
            return sequence;
        }
        
        public Sequence JumpInOut(Transform target, Vector3 offset, Vector3 startPosition, float scale)
        {
            var sequence = DOTween.Sequence(target);
            var offsetPosition = startPosition + offset;
            
            sequence.SetAutoKill();
            sequence
                .AppendCallback(() => target.position = scale == 0 ? startPosition : offsetPosition)
                .AppendInterval(AnimationConstants.OneTenth);
            
            sequence
                .Append(target
                    .DOScale(scale, _windowAnimationSettings.ScaleDuration))
                .Join(target
                    .DOMove(scale == 0 ? offsetPosition : startPosition, _windowAnimationSettings.JumpDuration))
                .AppendCallback(() => target.position = startPosition)
                .SetEase(_windowAnimationSettings.JumpCurve);
            
            return sequence;
        }
        
        public Sequence FloatInOut(CanvasGroup target, Vector3 offset, Vector3 startPosition, float alpha)
        {
            var sequence = DOTween.Sequence(target);
            var offsetPosition = startPosition + offset;
            var rectTransform = (RectTransform)target.transform;
            
            sequence.SetAutoKill();
            sequence
                .AppendCallback(() => target.transform.position = alpha == 0 ? startPosition : offsetPosition)
                .AppendCallback(() =>
                {
                    target.alpha = alpha == 0 ? 1 : 0;
                    var targetInteractable = alpha == 0;
                    target.interactable = target.blocksRaycasts = targetInteractable;
                })
                .AppendInterval(AnimationConstants.OneTenth);

            sequence
                .Append(target
                    .DOFade(alpha, _windowAnimationSettings.FadeDuration))
                .Join(rectTransform
                    .DOMove(alpha == 0 ? offsetPosition : startPosition,
                    _windowAnimationSettings.JumpDuration))
                .AppendCallback(() => target.transform.position = startPosition)
                .AppendCallback(() =>
                {
                    var targetInteractable = alpha != 0;
                    target.interactable = target.blocksRaycasts = targetInteractable;
                })
                .SetEase(_windowAnimationSettings.FadeCurve);
            
            return sequence;
        }
        
        public Sequence FloatInOut(Image target, Vector3 offset, Vector3 startPosition, float alpha)
        {
            var sequence = DOTween.Sequence(target);
            var offsetPosition = startPosition + offset;
            
            sequence.SetAutoKill();
            sequence
                .AppendCallback(() => target.transform.position = alpha == 0 ? startPosition : offsetPosition)
                .AppendCallback(() =>
                {
                    target.DOFade(alpha == 0 ? 1 : 0, 0f);
                    var targetInteractable = alpha == 0;
                    target.raycastTarget = targetInteractable;
                })
                .AppendInterval(AnimationConstants.OneTenth);

            sequence
                .Append(target
                    .DOFade(alpha, _windowAnimationSettings.FadeDuration))
                .Join(target.rectTransform
                    .DOMove(alpha == 0 ? offsetPosition : startPosition,
                        _windowAnimationSettings.JumpDuration))
                .AppendCallback(() => target.transform.position = startPosition)
                .AppendCallback(() =>
                {
                    var targetInteractable = alpha != 0;
                    target.raycastTarget = targetInteractable;
                })
                .SetEase(_windowAnimationSettings.FadeCurve);
            
            return sequence;
        }

        public Sequence SpriteChange(Image target, Sprite oldSprite, Sprite newSprite, float value)
        {
            var sequence = DOTween.Sequence(target);
            sequence.SetAutoKill();

            sequence
                .Append(target
                    .DOFade(0, _windowAnimationSettings.SpriteChangeDuration))
                .AppendCallback(() => target.sprite = value == 0 ? oldSprite: newSprite)
                .Join(target
                    .DOFade(1, _windowAnimationSettings.SpriteChangeDuration))
                .SetEase(_windowAnimationSettings.FadeCurve);
            
            return sequence;
        }

        public Sequence SizeInOut(RectTransform target, Vector2 originalSize, float value)
        {
            var sequence = DOTween.Sequence(target);
            var desiredSize = new Vector2(originalSize.y, originalSize.y);
            sequence.SetAutoKill();

            sequence
                .AppendCallback(() => target.sizeDelta = value == 0 ? originalSize : desiredSize)
                .AppendInterval(AnimationConstants.OneTenth)
                .Append(target.DOSizeDelta(value == 0 ? desiredSize : originalSize,
                    _windowAnimationSettings.SizeDuration))
                .SetEase(_windowAnimationSettings.SizeCurve);
            
            return sequence;
        }
    }
}