using System;
using System.Collections.Generic;

namespace Shared.Extensions.Rx
{
    public class CompositeDisposable
    {
        private List<IDisposable> _disposableList;

        public CompositeDisposable()
        {
            _disposableList = new List<IDisposable>();
        }

        public void Add(IDisposable item)
        {
            _disposableList.Add(item);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposableList)
            {
                disposable.Dispose();
            }
        }
    }
}