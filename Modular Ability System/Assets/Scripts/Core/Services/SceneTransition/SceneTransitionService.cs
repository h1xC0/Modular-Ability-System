using System;
using System.Collections;
using DG.Tweening;
using Shared.ExtensionTools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Services.SceneTransition
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneTransitionService : MonoBehaviour, ISceneTransitionService
    {
        public event Action SceneLoadEvent;

        [SerializeField] private float _fadeDelay = 3f;
        [SerializeField] private float _fadeDuration = 1f;
        
        [Header("References")]
        [SerializeField] private Image _loadingIcon;
        [SerializeField] private SpriteChanger _loadChanger;
        [SerializeField] private TMP_Text _percentsTMP;
        [SerializeField] private TMP_Text _loadingTMP;
        [SerializeField] private Canvas _canvas;

        private SceneTransitionProgress _sceneTransitionProgress;

        private CanvasGroup _canvasGroup;
        private bool _fading = true;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _sceneTransitionProgress = new SceneTransitionProgress(_percentsTMP, _loadChanger, _loadingTMP);
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1.5f);
            
            if(_fading == false)
                SceneLoadEvent?.Invoke();
        }

        public Sequence FadeIn()
        {
            var sequence = DOTween.Sequence();
            _fading = true;
            _canvas.gameObject.SetActive(true);
            _sceneTransitionProgress.SetProgression(1);

            sequence.Append(_canvasGroup
                .DOFade(1, _fadeDuration)
                .OnComplete(() =>
                {
                    _canvasGroup.blocksRaycasts = true;
                    _canvasGroup.interactable = true;
                }));
            return sequence;
        }

        public Sequence FadeOut()
        {
            var sequence = DOTween.Sequence();

            sequence.AppendCallback(() =>
            {
                _fading = false;
            });

            sequence.SetDelay(_fadeDelay);
            sequence.AppendCallback(() => SceneLoadEvent?.Invoke());
            sequence.Append(_loadingIcon.DOFillAmount(1, 1))
                .SetEase(Ease.InOutCubic);
            
            sequence.Append(_canvasGroup
                .DOFade(0, _fadeDuration)
                .OnComplete(() =>
                {
                    _canvasGroup.blocksRaycasts = false;
                    _canvasGroup.interactable = false;
                    _canvas.gameObject.SetActive(false);
                }));

            sequence.SetUpdate(true);

            return sequence;
        }
    }
}