using System;
using DG.Tweening;

namespace Core.Services.SceneTransition
{
    public interface ISceneTransitionService
    {
        event Action SceneLoadEvent;
        Sequence FadeIn();
        Sequence FadeOut();
    }
}