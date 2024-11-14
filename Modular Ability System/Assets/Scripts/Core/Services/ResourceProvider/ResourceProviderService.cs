using System;
using System.Collections.Generic;
using Shared.Constants;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Services.ResourceProvider
{
    public class ResourceProviderService : IResourceProviderService
    {
        public TResource LoadResource<TResource>(bool baseType = false) where TResource : Object, IResource
        {
            var resource = Resources.Load<TResource>(ResourceNames.GetPath<TResource>(baseType));
            if (resource != null)
            {
                return resource;
            }
                
            throw new NullReferenceException($"{typeof(TResource)} resource wasn't found");
        }

        public TResource LoadResource<TResource>(TResource tResource, bool baseType = false) where TResource : Object, IResource
        {
            var resource = Resources.Load<TResource>(ResourceNames.GetPath<TResource>(baseType));
            if (resource != null)
            {
                return resource;
            }
                
            throw new NullReferenceException($"{resource.GetType()} resource wasn't found");
        }

        public IEnumerable<TResource> LoadResources<TResource>(bool baseType = false)
            where TResource : Object, IResource
        {
            var resources = Resources.LoadAll<TResource>(ResourceNames.GetPath<TResource>(baseType));

            if (resources is { Length: > 0 })
            {
                if (resources is {Length: 1})
                {
                    Debug.LogWarning($"There is a resource with type {typeof(TResource)}");
                }
                
                return resources;
            }

            throw new NullReferenceException($"{typeof(TResource)} resources weren't found");
        }

        public IEnumerable<TResource> LoadResources<TResource>(TResource tResource, bool baseType = false)
            where TResource : Object, IResource
        {
            var resources = Resources.LoadAll<TResource>(ResourceNames.GetPath(tResource.GetType(), baseType));

            if (resources is { Length: > 0 })
            {
                if (resources is {Length: 1})
                {
                    Debug.LogWarning($"There is a resource with type {typeof(TResource)}");
                }
                
                return resources;
            }

            throw new NullReferenceException($"{typeof(TResource)} resources weren't found");
        }
    }
}
