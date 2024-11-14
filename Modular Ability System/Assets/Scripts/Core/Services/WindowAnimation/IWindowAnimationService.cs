using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Services.WindowAnimation
{
    public interface IWindowAnimationService
    {
        Sequence FadeInOut(CanvasGroup target, float alpha);
        Sequence FadeInOut(Image target, float alpha);
        Sequence ScaleInOut(Transform target, float scale);
        Sequence JumpInOut(Transform target, Vector3 offset, Vector3 startPosition, float scale);
        Sequence FloatInOut(CanvasGroup target, Vector3 offset, Vector3 startPosition, float alpha);
        Sequence FloatInOut(Image target, Vector3 offset, Vector3 startPosition, float alpha);
        Sequence SpriteChange(Image target, Sprite oldSprite, Sprite newSprite, float value);
        Sequence SizeInOut(RectTransform target, Vector2 originalSize, float value);
    }
}