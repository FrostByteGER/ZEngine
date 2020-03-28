using System;
using System.Collections.Generic;
using System.Linq;

namespace ZEngine.Engine.Services.Locator
{
    public abstract class AbstractServiceLocator<T> where T : IAbstractService
    {
        protected readonly Dictionary<ServiceData, T> Services = new Dictionary<ServiceData, T>();

        protected class ServiceData
        {
            public string Id { get; }
            public Type ServiceType { get; }

            public ServiceData(string id, Type serviceType)
            {
                Id = id;
                ServiceType = serviceType;
            }
        }

        protected virtual TU BaseGetService<TU>(string id = null) where TU : T
        {
            var type = typeof(TU);
            var result = Services.FirstOrDefault(e => id != null && id.Equals(e.Key.Id) || id == null && e.Key.ServiceType == type);
#if DEBUG
            // Don't check in release builds!
            if(result.Value == null && !type.IsInterface)
                throw new ArgumentException("Type TU " + type.Name + " with ID <" + id + "> is not an interface!");
#endif
            return (TU)result.Value;
        }

        protected virtual void BaseRegisterService<TU>(T service, string id = null) where TU : T
        {
            if (!Services.Any(e => (id != null && e.Key.Id != null && id.Equals(e.Key.Id)) ||
                                    (id == null && e.Key.ServiceType == typeof(TU))))
                Services.Add(new ServiceData(id, typeof(TU)), service);
            else
                throw new ArgumentException("Service " + service.GetType().Name + "with ID <" + id + "> is already registered!");
        }

        protected virtual void BaseUnregisterService<TU>(string id = null) where TU : T
        {
            var key = Services.FirstOrDefault(e =>
                id != null && e.Key.Id.Equals(id) || id == null && e.Key.ServiceType == typeof(TU)).Key;
            if (key != null)
                Services.Remove(key);
        }

        public void InitializeServices()
        {
            foreach (var service in Services.Values)
            {
                service.Initialize();
            }
        }

        public void DeinitializeServices()
        {
            foreach (var service in Services.Values)
            {
                service.Deinitialize();
            }
        }
    }
}