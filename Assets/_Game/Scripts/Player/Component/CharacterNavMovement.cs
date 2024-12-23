using System;
using UnityEngine;
using UnityEngine.AI;

namespace MH.Component
{
    public class CharacterNavMovement : EntityComponent
    {

        public enum MovementState
        {
            None,
            Moving,
            Idling
        }
        
        #region  ------------- Inspetors ---------------

        [SerializeField] private float _speed;

        #endregion

        #region  ---------- Properties ------------

        public Action LandedDestination;
        
        private NavMeshAgent _navMeshAgent;
        
        private MovementState _movementState;
        private Vector3 _currentDestination;
        private bool _isLocking;
        

        #endregion

        #region  --------- Entity Methods ------------

        public override void ManualStart()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _movementState = MovementState.Idling;
            _currentDestination = transform.position;
            
            _navMeshAgent.speed = _speed;
        }

        #endregion


        #region  ------------- Public Methods -------------

        public void MoveTo(Vector3 targetPos)
        {
            if(_isLocking) return;
            
            _movementState = MovementState.Moving;
            
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(targetPos);
            _navMeshAgent.speed = _speed;
        }

        public void Stop()
        {
            _movementState = MovementState.Idling;
            
            _navMeshAgent.isStopped = true;
            _navMeshAgent.speed = 0f;
        }


        public void SetLock(bool isLocking)
        {
            _isLocking = isLocking;
        }

        #endregion
    }
}