using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIExtensions.Selectables
{
    public class GridScrollSelectorView : ScrollSelectorView
    {
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        private void Awake()
        {
            EnableEvent += SelectButtonEnable;
        }

        private async void SelectButtonEnable()
        {
            await UniTask.WaitUntil(() => Initialized);
            SelectFirstButton();
        }

        public override void Dispose()
        {
            base.Dispose();
            EnableEvent -= SelectButtonEnable;
        }
    }
}