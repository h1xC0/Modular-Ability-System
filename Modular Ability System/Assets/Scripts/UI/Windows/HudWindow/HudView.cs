using System.Collections.Generic;
using Game.Combat.Abilities;
using UI.MVP.Advanced;
using UnityEngine;

namespace UI.Windows.HUDWindow
{
    public class HudView : WindowView, IHudView
    {
        [SerializeField] private RectTransform _abilitiesRoot;
        [SerializeField] private AbilityView _abilityViewPrefab;
        private List<IAbilityView> _abilityViews;
        
        public void Initialize(List<AbilityConfiguration> abilityConfigurations)
        {
            _abilityViews = new List<IAbilityView>();
            for (var i = 0; i < abilityConfigurations.Count; i++)
            {
                var configuration = abilityConfigurations[i];
                var abilityView = Instantiate(_abilityViewPrefab, _abilitiesRoot);
                abilityView.Initialize(i, configuration);
                _abilityViews.Add(abilityView);
            }
        }
    }
}
