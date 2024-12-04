using System;
using UnityEngine;

namespace Game.Combat.Abilities
{
    [Serializable]
    public class AreaAttackComponent : IAbilityComponent
    {
        public float Damage => _damage;
        [SerializeField] private float _damage;
        
        public void Execute()
        {
            
        }
    }
}