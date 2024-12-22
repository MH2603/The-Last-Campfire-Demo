using UnityEngine;
using System;
using System.Collections.Generic;

namespace MH
{
    // Interface that all game services should implement
    public interface IGameService
    {
        void Initialize();
        void Cleanup();
    }

    // Service initialization state tracker
    public class ServiceStatus
    {
        public bool IsInitialized { get; private set; }
        public IGameService Service { get; private set; }

        public ServiceStatus(IGameService service)
        {
            Service = service;
            IsInitialized = false;
        }

        public void MarkAsInitialized()
        {
            IsInitialized = true;
        }
    }

    // Main ServiceLocator class
    public class ServiceLocator : MonoBehaviour
    {
        #region Singleton
        private static ServiceLocator _instance;
        
        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("ServiceLocator");
                    _instance = go.AddComponent<ServiceLocator>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }
        #endregion

        private readonly Dictionary<Type, ServiceStatus> _services = new();
        private bool _isInitialized;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Initialize()
        {
            if (_isInitialized)
            {
                Debug.LogWarning("ServiceLocator is already initialized!");
                return;
            }

            foreach (var serviceStatus in _services.Values)
            {
                if (!serviceStatus.IsInitialized)
                {
                    serviceStatus.Service.Initialize();
                    serviceStatus.MarkAsInitialized();
                }
            }

            _isInitialized = true;
            Debug.Log("ServiceLocator initialized successfully!");
        }

        public void RegisterService<T>(T service) where T : class, IGameService
        {
            var serviceType = typeof(T);

            if (_services.ContainsKey(serviceType))
            {
                Debug.LogError($"Service of type {serviceType.Name} is already registered!");
                return;
            }

            _services[serviceType] = new ServiceStatus(service);
            Debug.Log($"Service {serviceType.Name} registered successfully!");
        }

        public T GetService<T>() where T : class, IGameService
        {
            var serviceType = typeof(T);

            if (!_services.TryGetValue(serviceType, out var serviceStatus))
            {
                Debug.LogError($"Service of type {serviceType.Name} not found!");
                return null;
            }

            return serviceStatus.Service as T;
        }

        public bool HasService<T>() where T : class, IGameService
        {
            return _services.ContainsKey(typeof(T));
        }

        public void UnregisterService<T>() where T : class, IGameService
        {
            var serviceType = typeof(T);

            if (!_services.TryGetValue(serviceType, out var serviceStatus))
            {
                Debug.LogWarning($"Attempting to unregister non-existent service of type {serviceType.Name}");
                return;
            }

            serviceStatus.Service.Cleanup();
            _services.Remove(serviceType);
            Debug.Log($"Service {serviceType.Name} unregistered successfully!");
        }

        private void OnDestroy()
        {
            foreach (var serviceStatus in _services.Values)
            {
                serviceStatus.Service.Cleanup();
            }
            _services.Clear();
            _isInitialized = false;
        }
    }
}