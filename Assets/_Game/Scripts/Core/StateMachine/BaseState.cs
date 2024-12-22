using System;

namespace MH.StateMachine
{
    /// <summary>
    /// Represents a state in a state machine with callbacks for various state transitions.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// This method is called once when the state machine is starting. Override this method in derived classes to perform actions like register to event.
        /// </summary>
        void OnStateStart();

        /// <summary>
        /// This method is called when the state entered.
        /// Implement this method in derived classes to perform actions specific to when the state is entered.
        /// </summary>
        void OnStateEnter();

        /// <summary>
        /// This method is called when the state machine is exiting the current state.
        /// Override this method in derived classes to perform actions before the state is exited, like cleaning up resources or saving data.
        /// </summary>
        void OnStateExit();

        /// <summary>
        /// This method is called every frame while the state is active. Override this method in derived classes to implement behavior that should be executed continuously during the state.
        /// </summary>
        void OnStateUpdate();
    }
    
    /// <summary>
    /// Represents a state in a state machine with callbacks for various state transitions.
    /// </summary>
    public interface IState<T> : IState where T : Enum
    {
        StateMachine<T> StateMachine { get; set; }
    }
    
    public abstract class BaseState<TEnum, TStateMachine> : IState<TEnum>
        where TEnum : Enum where TStateMachine : StateMachine<TEnum>
    {
        StateMachine<TEnum> IState<TEnum>.StateMachine { get; set; }
        protected TStateMachine           StateMachine => (TStateMachine)((IState<TEnum>)(this)).StateMachine;

        protected BaseState(TStateMachine stateMachine)
        {
            ((IState<TEnum>)this).StateMachine = stateMachine;
        }

        public virtual void OnStateStart() {}
        public virtual void OnStateEnter() {}
        public virtual void OnStateExit() {}
        public virtual void OnStateUpdate() {}
    }
}