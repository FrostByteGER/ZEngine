using System;
using System.Collections.Generic;
using System.Linq;

namespace SFML_Engine.Engine.Services
{
    public class ServiceLocator
    {
        private class ServiceData
        {
            public string Id { get; }
            public Type ServiceType { get; }

            public ServiceData(string id, Type serviceType)
            {
                Id = id;
                ServiceType = serviceType;
            }
        }

        public static ServiceLocator Instance { get; } = new ServiceLocator();

        private readonly Dictionary<ServiceData, IService> _services;

        public ServiceLocator()
        {
            _services = new Dictionary<ServiceData, IService>();
        }

        public static T GetService<T>(string id = null) where T : class
        {
            return Instance._services.FirstOrDefault(e => id != null && e.Key.Id.Equals(id) || id == null && e.Key.ServiceType == typeof(T)).Value as T;
        }

        public static void RegisterService<T>(IService service, string id = null) where T : class
        {
            if (!Instance._services.Any(e => (id != null && e.Key.Id != null && id.Equals(e.Key.Id)) ||
                                            (id == null && e.Key.ServiceType == typeof(T))))
                Instance._services.Add(new ServiceData(id, typeof(T)), service);
            else
                throw new ArgumentException("Service " + service.GetType().Name + "with ID " + id + " is already registered!");
        }

        public static void UnregisterService<T>(string id = null) where T : class
        {
            var key = Instance._services.FirstOrDefault(e =>
                id != null && e.Key.Id.Equals(id) || id == null && e.Key.ServiceType == typeof(T)).Key;
            if (key != null)
                Instance._services.Remove(key);
        }
    }
}