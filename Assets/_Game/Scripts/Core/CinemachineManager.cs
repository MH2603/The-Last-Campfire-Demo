using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace MH
{
    [Serializable]
    public struct CinemachineRefConfig
    {
        public string Name;
        public CinemachineVirtualCamera VirtualCamera;
    }

    public  interface ICinemachineService : IGameService
    {
        public void Initialize()
        {
            
        }

        public void Cleanup()
        {
            
        }

        public void UseDefaultCinemachine();
        public void UseCinemachine(string cinemachineName);
    }
    
    public class CinemachineManager : MonoBehaviour, ICinemachineService
    {
        #region ---------- Inpectors -------------

        [SerializeField] private CinemachineVirtualCamera _defaultCinemachine;
        [SerializeField] private CinemachineRefConfig[] _configs;

        #endregion

        private Dictionary<string, CinemachineVirtualCamera> _cinemachineMap = new();

        protected void Start()
        {
            foreach (var config in _configs)
            {
                _cinemachineMap.Add(config.Name, config.VirtualCamera);
            }
            ServiceLocator.Instance.RegisterService<ICinemachineService>(this);
        }

        public void Initialize()
        {
            
        }

        public void Cleanup()
        {
            
        }

        public void UseDefaultCinemachine()
        {
            DownAllCinemachinesPriority();
            _defaultCinemachine.Priority = 10;
        }

        public void UseCinemachine(string cinemachineName)
        {
            if ( !_cinemachineMap.ContainsKey(cinemachineName) )
            {
                Debug.LogError("Cinemachine not found: " + cinemachineName);
                return;
            }

            DownAllCinemachinesPriority();
            _cinemachineMap[cinemachineName].Priority = 10;
        }
        
        public void UseCinemachine(CinemachineVirtualCamera cinemachine)
        {
 
            DownAllCinemachinesPriority();
            cinemachine.Priority = 10;
        }


        private void DownAllCinemachinesPriority()
        {
            foreach ( var cinemachine in _cinemachineMap.Values )
            {
                cinemachine.Priority = 0;
            }
        }
    }
}