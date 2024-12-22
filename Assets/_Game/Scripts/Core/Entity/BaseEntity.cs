using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MH
{

    public abstract class BaseEntity : MonoBehaviour  
    {
        #region ----------- Inspectors ----------

        [SerializeField] protected EntityComponent[] _entityComponents;

        #endregion

        #region --------------- Properties -------------

        protected Dictionary<Type, EntityComponent> _componentMap;

        #endregion

        #region  ------------- Unty Methods ------------

        protected virtual void Awake()
        {
            _componentMap = new();
            for(int i = 0; i < _entityComponents.Length; i++)
            {
                _entityComponents[i].SetUp(this);
                _componentMap.Add(_entityComponents[i].GetType(), _entityComponents[i]);
            }   
        }

        protected virtual void Start()
        {
            for (int i = 0; i < _entityComponents.Length; i++)
            {
                _entityComponents[i].ManualStart();
            }
        }

        protected virtual void Update()
        {
            for (int i = 0; i < _entityComponents.Length; i++)
            {
                _entityComponents[i].ManualUpdate();
            }
        }

        protected virtual void FixedUpdate()
        {
            for (int i = 0; i < _entityComponents.Length; i++)
            {
                _entityComponents[i].ManualFixedUpdate();
            }
        }

        #endregion

        #region -------- Public Methods -----------

        public TEntityComponent Get<TEntityComponent>() where TEntityComponent : EntityComponent
        {
            if (!_componentMap.ContainsKey( typeof(TEntityComponent) ))
            {
                Debug.LogError($" Error get entity component={typeof(TEntityComponent)} at entity={gameObject.name} ");
                return default;
            }
            return (TEntityComponent)_componentMap[typeof(TEntityComponent)];
        }

        #endregion
    }

}
