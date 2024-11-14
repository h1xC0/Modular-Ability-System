using Core.Systems.CommandSystem;
using Shared.Constants;

namespace Core.CommandBindings.Payloads
{
    public sealed class SceneNamePayload : ICommandPayload
    {
        public readonly SceneInfo SceneInfoLoad;
        public readonly SceneInfo SceneInfoUnload;

        public SceneNamePayload(SceneInfo sceneInfoLoad, SceneInfo sceneInfoUnload = null)
        {
            SceneInfoLoad = sceneInfoLoad;
            SceneInfoUnload = sceneInfoUnload;
        }
    }
}