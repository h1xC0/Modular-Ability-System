using Game.Combat.Abilities;
using UI.MVP;

namespace UI.Windows.HUDWindow
{
    public interface IAbilityView : IView
    {
        void Initialize(int index, AbilityConfiguration abilityConfiguration);
    }
}