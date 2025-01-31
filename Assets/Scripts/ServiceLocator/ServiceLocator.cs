using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocations
{
    public class ServiceLocator
    {
        public static ServiceLocator Context;

        private Dictionary<Type, IService> services;

        public ServiceLocator()
        {
            services = new Dictionary<Type, IService>();
            if (Context != null)
                Debug.LogError("Duplicate creation of ServiceLocator");

            Context = this;
        }

        public void BindSingle<T>(T service) where T : IService
        {
            if (services.ContainsValue(service))
            {
                Debug.LogError("Service already exist");
                return;
            }

            services.Add(service.GetType(), service);
        }

        public void Replace<T>(T service) where T : IService
        {
            if (services.ContainsValue(service))
                services[service.GetType()] = service;
            else
                services.Add(service.GetType(), service);
        }

        public T GetSingle<T>() where T : IService
        {
            var type = typeof(T);
            if (services.ContainsKey(type))
                return (T)services[typeof(T)];
            else
            {
                Debug.LogError("Trying to get component that is not Binded");
                return default;
            }
        }
    }
}