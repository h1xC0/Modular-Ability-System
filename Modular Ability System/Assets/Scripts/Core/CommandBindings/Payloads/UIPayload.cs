using Core.Systems.CommandSystem;
using Zenject;

namespace Core.CommandBindings.Payloads
{
    public class UIPayload : ICommandPayload
    {
        public DiContainer Container;

        public UIPayload(DiContainer container)
        {
            Container = container;
        }
    }
}