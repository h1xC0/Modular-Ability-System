using Shared.Extensions.Rx;
using UnityEngine;

namespace Game.Combat
{
    public class HealthComponent : IHealthComponent
    {
        public IReactiveProperty<float> Health => _health;
        private ReactiveProperty<float> _health;

        public HealthComponent()
        {
            _health = new ReactiveProperty<float>(100);
        }
        
        public void Heal(float value)
        {
            SetHealthPoints(value);
        }
        
        public void Damage(float value)
        {
            SetHealthPoints(-value);
        }

        private void SetHealthPoints(float value)
        {
            _health.Value = Mathf.Clamp(_health.Value + value, 0, 100);
        }
    }

    public interface IHealthComponent
    {
        void Heal(float value);
        void Damage(float value);
    }
}