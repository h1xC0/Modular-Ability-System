using System;

namespace Game.Combat.Abilities
{
    [Serializable]
    public class MovingComponent : IAbilityComponent
    {
        public bool _lookAtTarget;
        
        public bool _moveForward;
        public float _moveStep;
        
        public void Execute()
        {
            
        }
    }
}