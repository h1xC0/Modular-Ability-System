using Core.CommandBindings.Payloads;
using Core.Services.SceneTransition;
using Core.Systems.CommandSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.CommandBindings.Commands
{
    public class UnloadSceneCommand : Command
    {
        private readonly ISceneTransitionService _sceneTransitionService;
        private readonly ICommandDispatcher _commandDispatcher;

        private SceneNamePayload _scenePayload;
        
        public UnloadSceneCommand(ISceneTransitionService sceneTransitionService, ICommandDispatcher commandDispatcher)
        {
            _sceneTransitionService = sceneTransitionService;
            _commandDispatcher = commandDispatcher;
        }

        protected override void Execute(ICommandPayload payload)
        {
            Retain();
            
            _scenePayload = payload as SceneNamePayload;
            if (_scenePayload?.SceneInfoUnload == null)
            {
                var unloadCurrentSceneOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
                unloadCurrentSceneOperation.completed += ReleaseCommand;
                
                Release();
                return;
            }

            var unloadSceneOperation = SceneManager.UnloadSceneAsync(_scenePayload.SceneInfoUnload.Title);
            unloadSceneOperation.completed += ReleaseCommand;
            
            Release();
        }
        
        private void ReleaseCommand(AsyncOperation operation)
        {
            operation.completed -= ReleaseCommand;
            _sceneTransitionService.FadeOut();
            Release();
        }
    }
}