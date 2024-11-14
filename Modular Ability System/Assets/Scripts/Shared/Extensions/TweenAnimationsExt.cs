using DG.Tweening;
using Shared.Constants;
using Shared.Enums;
using UnityEngine;

namespace Shared.Extensions
{
    public static class TweenAnimationsExt
    {
        private static readonly int Fill = Shader.PropertyToID("_Fill");
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

        public static Sequence TakeAndRotatePickupItemSequence(this Transform target, Transform destination, Quaternion rotation)
        {
            var sequence = DOTween.Sequence(target);
            
            sequence.Append(target.DOMove(destination.position, AnimationConstants.Quarter));
            sequence.Join(target.DORotateQuaternion(rotation, AnimationConstants.Quarter));
            
            return sequence;
        }
        
        public static Sequence PlacePickupItemSequence(this Transform target, Vector3 destination, Vector3 rotation = default)
        {
            var sequence = DOTween.Sequence(target);
            sequence.Append(target.DOMove(destination, AnimationConstants.Quarter));
            sequence.Join(target.DORotate(rotation, AnimationConstants.Quarter));

            return sequence;
        }
        
        public static Sequence LocalPlacePickupItemSequence(this Transform target, Vector3 destination, Vector3 rotation = default)
        {
            var sequence = DOTween.Sequence(target);
            sequence.Append(target.DOLocalMove(destination, AnimationConstants.Quarter));
            sequence.Join(target.DOLocalRotate(rotation, AnimationConstants.Quarter));

            return sequence;
        }

        public static Sequence PushButton(this Transform target, Vector3Axis axis, float strength, Ease ease)
        {
            var sequence = DOTween.Sequence(target);

            sequence
                .SetAutoKill()
                .SetEase(ease);
            
            switch (axis)
            {
                case Vector3Axis.X:
                    sequence.Append(target.DOMoveX(target.position.x + strength, AnimationConstants.Quarter));
                    break;
                case Vector3Axis.Y:
                    sequence.Append(target.DOMoveY(target.position.y + strength, AnimationConstants.Quarter));
                    break;
                case Vector3Axis.Z:
                    sequence.Append(target.DOMoveZ(target.position.z + strength, AnimationConstants.Quarter));
                    break;
            }

            return sequence;
        }

        public static Sequence RecoverButtonPose(this Transform target, Vector3 originalPosition)
        {
            var sequence = DOTween.Sequence(target);
            sequence
                .Append(target
                    .DOMove(originalPosition, AnimationConstants.Quarter)
                    .SetDelay(AnimationConstants.Quarter));

            return sequence;
        }

        public static Sequence JumpToTarget(this Transform source, Transform target, float speed)
        {
            var sequence = DOTween.Sequence(target);
            var duration = (source.position - target.position).sqrMagnitude / speed;  
            duration = Mathf.Clamp(duration / 1.5f, .5f, 35);
            
            sequence
                .Append(source.DOJump(target.position, duration, 1, duration))
                .Join(source.DORotate(target.eulerAngles, duration));
            
            return sequence;
        }
        
        public static Sequence JumpTo(this Transform source, Vector3 destination, float speed)
        {
            var sequence = DOTween.Sequence(destination);
            var duration = (source.position - destination).sqrMagnitude / speed;  
            duration = Mathf.Clamp(duration / 1.5f, .5f, 35);

            sequence
                .Append(source.DOJump(destination, duration, 1, duration));
            
            return sequence;
        }
        
        public static Sequence MoveToTarget(this Transform source, Transform target, float duration)
        {
            var sequence = DOTween.Sequence(target);

            sequence
                .Append(source.DOMove(target.position, duration))
                .Join(source.DORotate(target.eulerAngles, duration));
            
            return sequence;
        }

        public static Sequence LocalMoveToTarget(this Transform source, Transform target, float duration)
        {
            var sequence = DOTween.Sequence(target);

            sequence
                .Append(source.DOLocalMove(target.localPosition, duration))
                .Join(source.DOLocalRotate(target.localEulerAngles, duration));
            
            return sequence;
        }

        public static Sequence LocalRotateAround360(this Transform target, Vector3 eulerAngles, float duration)
        {
            var sequence = DOTween.Sequence(target);

            sequence.Append(target.DOLocalRotate(new Vector3(0, 360, 0), duration, RotateMode.WorldAxisAdd));
            sequence.Append(target.DOLocalRotate(new Vector3(0, 360, 0), duration, RotateMode.WorldAxisAdd));
            sequence.Join(target.DOLocalRotate(eulerAngles, duration, RotateMode.FastBeyond360));

            sequence.SetEase(Ease.InOutQuad);
            return sequence;
        }

        public static Sequence ColorTo(this MaterialPropertyBlock propertyBlock, Renderer renderer, Color to, float duration)
        {
            renderer.GetPropertyBlock(propertyBlock);
            
            var from = renderer.HasPropertyBlock() ?
                propertyBlock.GetColor(BaseColor)
                : renderer.sharedMaterial.GetColor(BaseColor);
            
            var sequence = DOTween.Sequence();
            sequence
                .Append(DOVirtual.Color(from, to, duration, value =>
                {
                    propertyBlock.SetColor(BaseColor, value);
                    renderer.SetPropertyBlock(propertyBlock);
                }));
            return sequence;
        }

        private static Sequence MoveToTargetLocal(this Transform target, TRSMatrix trs, float duration)
        {
            var sequence = DOTween.Sequence();

            sequence
                .Append(target.DOLocalMove(trs.Position, duration))
                .Join(target.DOLocalRotate(trs.Rotation, duration));
            
            return sequence;
        }
    }
}