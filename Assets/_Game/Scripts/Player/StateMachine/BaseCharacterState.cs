using MH.Component;

namespace MH.StateMachine
{
    public class BaseCharacterState : BaseState<CharacterState,PlayerStateMachine>
    {
        #region ---------- Propeties -------------

        protected BaseEntity Entity;
        protected CharacterNavMovement NavMovement;

        #endregion
        
        public BaseCharacterState(BaseEntity baseEntity ,PlayerStateMachine stateMachine) : base(stateMachine)
        {
            Entity = baseEntity;

            NavMovement = Entity.Get<CharacterNavMovement>();
        }
    }
}