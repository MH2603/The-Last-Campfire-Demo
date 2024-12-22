using MH.StateMachine;

namespace MH.Component
{
    public class PlayerStateMachineController : EntityComponent
    {
        private PlayerStateMachine stateMachine;

        public override void ManualStart()
        {
            stateMachine = new(baseEntity);
        }

        public override void ManualUpdate()
        {
            stateMachine.Update();
        }
    }
}