using Core.CommandBindings.Payloads;
using Core.Services.SceneTransition;
using Core.Systems.CommandSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.CommandBindings.Commands
{
    public class LoadSceneCommand : Command
    {
        private readonly ZenjectSceneLoader _sceneLoader;
        private readonly ISceneTransitionService _sceneTransitionService;
        private SceneNamePayload _scene;

        public LoadSceneCommand(ZenjectSceneLoader sceneLoader, ISceneTransitionService sceneTransitionService)
        {
            _sceneLoader = sceneLoader;
            _sceneTransitionService = sceneTransitionService;
        }

        protected override void Execute(ICommandPayload payload)
        {
            Retain();
            _scene = payload as SceneNamePayload;

            if (_scene is null)
            {
                Release();
                return;
            }

            _sceneTransitionService.FadeIn().AppendCallback(() =>
            {
                var loadSceneOperation = _sceneLoader.LoadSceneAsync(_scene.SceneInfoLoad.Title, LoadSceneMode.Additive, null, LoadSceneRelationship.Child);
                loadSceneOperation.completed += ReleaseCommand;
            });
        }

        private void ReleaseCommand(AsyncOperation operation)
        {
            operation.completed -= ReleaseCommand;
            Release();
        }
    }
}