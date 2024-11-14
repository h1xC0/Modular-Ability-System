using System.Text;
using DG.Tweening;
using Shared.Constants;
using Shared.ExtensionTools;
using TMPro;

namespace Core.Services.SceneTransition
{
    public class SceneTransitionProgress
    {
        private readonly TMP_Text _percents;
        private readonly SpriteChanger _loadChanger;
        private readonly TMP_Text _loading;

        private Sequence _loadingTMPSequence;

        public SceneTransitionProgress(TMP_Text percents, SpriteChanger loadChanger, TMP_Text loading)
        {
            _percents = percents;
            _loadChanger = loadChanger;
            _loading = loading;
        }

        public void SetProgression(float value)
        {
            DOVirtual.Float(0, value, 2, GetPercentage)
                .OnComplete(() =>
                {
                    _loadingTMPSequence.Kill();
                    _loadChanger.Pause();
                    
                });

            _loadingTMPSequence = DOTween.Sequence();
            var sb = new StringBuilder("Loading");
            for (var i = 0; i < 3; i++)
            {
                _loadingTMPSequence.AppendCallback(() =>
                {
                    sb.Append(".");
                    _loading.text = sb.ToString();
                });
                _loadingTMPSequence.AppendInterval(AnimationConstants.Half);
            }

            _loadingTMPSequence.AppendCallback(() =>
            {
                sb = new StringBuilder("Loading");
                _loading.text = sb.ToString();
            });
            _loadingTMPSequence.SetLoops(-1);
            _loadingTMPSequence.Play();
        }

        private void GetPercentage(float value)
        {
            _percents.text = $"{value * 100:N0}%";
            _loadChanger.Play();
        }
    }
}