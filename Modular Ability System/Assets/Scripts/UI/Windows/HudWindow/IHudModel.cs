using Game.Combat.Abilities;
using Shared.Extensions.Rx;
using UI.MVP;

namespace UI.Windows.HUDWindow
{
    public interface IHudModel : IModel
    {
        IReactiveCollection<AbilityConfiguration> AbilityConfigurations { get; }
    }
}