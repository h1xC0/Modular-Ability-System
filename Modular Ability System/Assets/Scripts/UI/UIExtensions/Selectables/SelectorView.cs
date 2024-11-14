using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Shared.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.UIExtensions.Selectables
{
    public class SelectorView : MonoBehaviour, ISelectorView
    {
        public bool Initialized { get; private set; }
        public event Action<SelectableOption> SelectedEvent;
        public event Action EnableEvent;

        [SerializeField] protected List<SelectableOption> _selectables;
        [SerializeField] private Navigation.Mode _navigationMode;

        private EventSystem _eventSystem;
        private CancellationTokenSource _cancelTokenSource;

        public virtual void Initialize()
        {
            if (_selectables.Count < 1)
                throw new NullReferenceException("Please add items in your list");
            
            foreach (var selectableOption in _selectables)
            {
                selectableOption.Initialize();
                selectableOption.DisposeEvent += CancelFirstButton;
                selectableOption.SelectedEvent += flag =>
                {
                    if(flag)
                        SelectedEvent?.Invoke(selectableOption);
                };
            }

            SelectFirstButton();

            if (_navigationMode is Navigation.Mode.None or Navigation.Mode.Automatic)
                return;
            
            SetupNavigation();
            Initialized = true;
        }

        public void OnEnable()
        {
            EnableEvent?.Invoke();
        }

        public void Add(SelectableOption selectableOption)
        {
            _selectables.Add(selectableOption);
        }

        public void Remove(SelectableOption selectableOption)
        {
            _selectables.Remove(selectableOption);
            SelectNext();
        }

        public void Clear()
        {
            _selectables.Clear();
        }

        public void SetOrder(IEnumerable<SelectableOption> list)
        {
            _selectables = list.ToList();
        }

        public void Select(SelectableOption selectableOption)
        {
            DeselectAll();
            _eventSystem.SetSelectedGameObject(selectableOption.gameObject);
        }

        private void DeselectAll()
        {
            _selectables.ForEach(selectable => selectable.OnDeselect(new BaseEventData(_eventSystem)));
        }

        public void Enable()
        {
            SelectFirstButton();
            SetNavigation(Navigation.Mode.Automatic);
        }

        public void Disable()
        {
            CancelFirstButton();
            if (_navigationMode is Navigation.Mode.None or Navigation.Mode.Automatic)
            {
                var eventData = new BaseEventData(_eventSystem);
                _selectables.Each(option => option.OnDeselect(eventData));
                return;
            }
            
            SetNavigation(Navigation.Mode.None);
        }

        private void SelectNext()
        {
            var firstInteractable = _selectables
                .Select(selectableOption => selectableOption.Selectable)
                .FirstOrDefault(selectable => selectable.interactable);
            
            _eventSystem.SetSelectedGameObject(firstInteractable?.gameObject);
        }

        protected async void SelectFirstButton()
        {
            _cancelTokenSource = new CancellationTokenSource(); 
            var token = _cancelTokenSource.Token;
            
            await UniTask.WaitUntil(() => _selectables.Any(option => option != null && option.gameObject.activeInHierarchy), PlayerLoopTiming.Update, token);

            var firstOption = _selectables.FirstOrDefault(option => option.Selectable.interactable);
            if (firstOption == null)
            {
                Debug.Log("<color=yellow>No button to select first</color>");
                return;
            }

            _eventSystem = EventSystem.current;
            _eventSystem.firstSelectedGameObject = firstOption.gameObject;
            _eventSystem.SetSelectedGameObject(firstOption.gameObject);
        }

        private void CancelFirstButton()
        {
            _cancelTokenSource?.Cancel(true);
        }

        private void SetupNavigation()
        {
            if (_selectables.Count < 2)
                return;
            
            _selectables[0].SetupNavigation(_selectables[^1].Selectable, _selectables[1].Selectable, _navigationMode);
            _selectables[^1].SetupNavigation(_selectables[^2].Selectable, _selectables[0].Selectable, _navigationMode);
        }

        private void SetNavigation(Navigation.Mode navigationMode)
        {
            for (var i = 0; i < _selectables.Count; i++)
            {
                var selectableOption = _selectables[i];
                Navigation navigation = selectableOption.Selectable.navigation;

                var explicitCondition = (i == 0 || i == _selectables.Count - 1) 
                                        && navigationMode == Navigation.Mode.Automatic 
                                        && _navigationMode is not (Navigation.Mode.Automatic or Navigation.Mode.None);
                
                navigation.mode = explicitCondition ? Navigation.Mode.Explicit : navigationMode;
                
                selectableOption.Selectable.navigation = navigation;
            }
        }

        public virtual void Dispose()
        {
            foreach (var selectableOption in _selectables)
            {
                selectableOption.SelectedEvent -= flag =>
                {
                    if(flag)
                        SelectedEvent?.Invoke(selectableOption);
                };

                selectableOption.DisposeEvent -= CancelFirstButton;
            }
        }
    }
}
