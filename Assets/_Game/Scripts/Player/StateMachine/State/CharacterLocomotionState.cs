using MH.Component;
using UnityEngine;

namespace MH.StateMachine
{
    public class CharacterLocomotionState : BaseCharacterState
    {
        
        private MouseRaycastHandler MouseRaycast;
        
        public CharacterLocomotionState(BaseEntity baseEntity, PlayerStateMachine stateMachine) : base(baseEntity, stateMachine)
        {
            MouseRaycast = Entity.Get<MouseRaycastHandler>();
        }

        public override void OnStateEnter()
        {
            MouseRaycast.OnRaycastHit += OnRaycastHit;
        }

        public override void OnStateExit()
        {
            MouseRaycast.OnRaycastHit -= OnRaycastHit;
        }

        private void OnRaycastHit(RaycastHit hitInfo)
        {
            if (hitInfo.collider.gameObject &&
                hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                CommandMoveTo(hitInfo.point);
            }
        }

        private void CommandMoveTo(Vector3 position)
        {
            NavMovement.MoveTo(position);
        }
    }
}