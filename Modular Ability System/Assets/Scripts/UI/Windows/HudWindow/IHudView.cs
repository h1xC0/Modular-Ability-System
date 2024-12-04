using System.Collections.Generic;
using Game.Combat.Abilities;
using UI.MVP.Advanced;

namespace UI.Windows.HUDWindow
{
    public interface IHudView : IWindowView
    {
        void Initialize(List<AbilityConfiguration> abilityConfigurations);
    }
}