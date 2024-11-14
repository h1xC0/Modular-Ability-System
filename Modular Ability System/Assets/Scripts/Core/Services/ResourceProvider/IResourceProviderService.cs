using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Core.Services.ResourceProvider
{
    public interface IResourceProviderService
    {
        TResource LoadResource<TResource>(bool baseType = false) where TResource : Object, IResource;
        TResource LoadResource<TResource>(TResource tResource, bool baseType = false) where TResource : Object, IResource;
        IEnumerable<TResource> LoadResources<TResource>(bool baseType = false) where TResource : Object, IResource;
        IEnumerable<TResource> LoadResources<TResource>(TResource tResource, bool baseType = false) where TResource : Object, IResource;
    }
}