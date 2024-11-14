using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Shared.Factories
{
    public abstract class AbstractFactory
    {
        protected DiContainer DiContainer { get; private set; }

        protected AbstractFactory(DiContainer diContainer)
        {
            DiContainer = diContainer;
        }

        protected T CreateObject<T>(string path, Transform parent = null)
            where T : Object
        {
            T prototype = Resources.Load<T>(path);
            if (prototype == null)
            {
                throw new NullReferenceException($"Couldn't find {path} object");
            }
            
            T objectInstance =(parent == null)? 
                Object.Instantiate(prototype):
                Object.Instantiate(prototype, parent);
            
            
            DiContainer.Inject(objectInstance);

            return objectInstance;
        }
        
        protected T CreateObject<T>(GameObject prefab, Transform parent = null)
            where T : Object
        {
            if (prefab == null)
            {
                throw new NullReferenceException($"Prefab reference is null");
            }
            
            T prototype = prefab.GetComponent<T>();
            if (prototype == null)
            {
                throw new NullReferenceException($"Prefab has no {typeof(T)} component");
            }
            T objectInstance = (parent == null)? 
                Object.Instantiate(prototype):
                Object.Instantiate(prototype, parent);
            
            DiContainer.Inject(objectInstance);

            return objectInstance;
        }
        
        protected T CreateObject<T>(Component component, Transform parent = null)
            where T : Object
        {
            if (component == null)
            {
                throw new NullReferenceException($"Prefab reference is null");
            }
            
            T prototype = component.GetComponent<T>();
            if (prototype == null)
            {
                throw new NullReferenceException($"Prefab has no {typeof(T)} component");
            }
            T objectInstance = (parent == null)? 
                Object.Instantiate(prototype):
                Object.Instantiate(prototype, parent);
            
            DiContainer.Inject(objectInstance);

            return objectInstance;
        }
    }
}