using System.Collections.Generic;

namespace Core.Systems.EventBus
{
    public class SubscribersCollection<TSubscriber> : ISubscribersCollection<TSubscriber> where TSubscriber : class, ISubscriber 
    {
        private bool _needsCleanUp = false;

        public bool Executing { get; set; }

        private readonly List<TSubscriber> _subscribersList;

        public SubscribersCollection()
        {
            _subscribersList = new List<TSubscriber>();
        }


        public void Add(TSubscriber subscriber)
        {
            _subscribersList.Add(subscriber);
        }

        public void Remove(TSubscriber subscriber) 
        {
            if (Executing)
            {
                var i = _subscribersList.IndexOf(subscriber);
                if (i >= 0)
                {
                    _needsCleanUp = true;
                    _subscribersList[i] = null;
                }
            }
            else
            {
                _subscribersList.Remove(subscriber);
            }
        }
        
        public void Cleanup()
        {
            if (!_needsCleanUp)
            {
                return;
            }

            _subscribersList.RemoveAll(s => s == null);
            _needsCleanUp = false;
        }

        public IEnumerable<TSubscriber> GetCollection()
        {
            return _subscribersList;
        }

        public void Dispose()
        {
            _subscribersList.Clear();
        }
    }
}