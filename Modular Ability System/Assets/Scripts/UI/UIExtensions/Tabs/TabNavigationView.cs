using System;
using System.Collections.Generic;
using Shared.Extensions.Rx;
using UnityEngine;

namespace UI.UIExtensions.Tabs
{
    public class TabNavigationView : RawView, ITabNavigationView
    {
        public List<TabView> TabList => _tabsList;
        [SerializeField] private List<TabView> _tabsList;
        private ReactiveProperty<int> _currentIndex;
        public event Action<TabView> TabChangeEvent;

        public override void Initialize()
        {
            base.Initialize();
            _currentIndex = new ReactiveProperty<int>();
            _currentIndex.Subscribe(SelectCurrentElement);
            _currentIndex.Value = 0;

            for (var i = 0; i < _tabsList.Count; i++)
            {
                var tab = _tabsList[i];
                var index = i;
                tab.TabSelectedEvent += () => OnMouseSelection(index);
            }
        }

        public void SetTabNavigation(bool flag)
        {
            if (_tabsList.Count == 0)
                return;
            
            if (flag)
                _tabsList[_currentIndex.Value].Content?.EnableNavigation();
            else
                _tabsList[_currentIndex.Value].Content?.DisableNavigation();
        }

        public void NextTab()
        {
            if (_currentIndex.Value >= _tabsList.Count - 1)
            {
                _currentIndex.Value = 0;
                return;
            }

            SetTabNavigation(false);
            _currentIndex.Value++;
            SetTabNavigation(true);
        }

        public void PreviousTab()
        {
            if (_currentIndex.Value <= 0)
            {
                _currentIndex.Value = _tabsList.Count - 1;
                return;
            }

            SetTabNavigation(false);
            _currentIndex.Value--;
            SetTabNavigation(true);
        }

        public TabView GetSelectedTab() =>
            _tabsList[_currentIndex.Value];

        private void SelectCurrentElement(int index)
        {
            foreach (var tab in _tabsList)
            {
                tab.OnDeselect();
            }

            _tabsList[index].OnSelect();
            TabChangeEvent?.Invoke(_tabsList[index]);
        }

        private void OnMouseSelection(int i)
        {
            SetTabNavigation(false);

            _currentIndex.ForceSetValue(i);
            SelectCurrentElement(i);

            SetTabNavigation(true);
        }
    }
}