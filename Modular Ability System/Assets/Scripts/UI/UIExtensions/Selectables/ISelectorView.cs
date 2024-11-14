using System;
using System.Collections.Generic;

namespace UI.UIExtensions.Selectables
{
    public interface ISelectorView : IDisposable
    {
        bool Initialized { get; }
        void Initialize();
        event Action EnableEvent;
        event Action<SelectableOption> SelectedEvent;
        void Add(SelectableOption selectableOption);
        void Remove(SelectableOption selectableOption);
        void Enable();
        void Disable();
        void Clear();
        void SetOrder(IEnumerable<SelectableOption> list);
        void Select(SelectableOption selectableOption);
    }
}