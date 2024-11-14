using UI.MVP;
using UnityEngine;

namespace UI
{
    public abstract class RawView : MonoBehaviour, IView
    {
        private bool _disposed;

        public virtual void Enable()
        {
            _disposed = false;
        }

        public virtual void Initialize()
        {
        
        }

        public virtual void Disable()
        {
        }

        public virtual void Dispose()
        {
            _disposed = true;
        }

        public virtual void OnDestroy()
        {
            if (_disposed) return;
            Dispose();
        }
    }
}