using System;
using UnityEngine;

namespace Game.Combat.Abilities
{
    [Serializable]
    public class AnimationComponent : IAbilityComponent
    {
        [SerializeField] private AnimationClip _animationClip;
        public void Execute()
        {
            
        }
    }
}