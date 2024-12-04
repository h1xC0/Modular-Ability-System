using System;
using Core.Services.SoundService;
using UnityEngine;

namespace Game.Combat.Abilities
{
    [Serializable]
    public class AudioComponent : IAbilityComponent
    {
        public SoundTitle SoundTitle => _soundTitle;
        [SerializeField] private SoundTitle _soundTitle;
        
        public void Execute()
        {
            
        }
    }
}