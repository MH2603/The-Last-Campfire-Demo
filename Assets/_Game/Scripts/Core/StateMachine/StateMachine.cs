using System;
using System.Collections.Generic;
using System.Linq;

namespace MH.StateMachine
{
    /// <summary>
    /// Represents a state machine that manages different states and transitions between them.
    /// </summary>
    /// <typeparam name="T">The enum type representing the states.</typeparam>
    public class StateMachine<T> where T : System.Enum
    {
        #region Fields

        // A dictionary that maps states to their respective state objects.
        private Dictionary<T, IState> _statesMap = new();
        
        private T _currentState;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current state of the state machine.
        /// </summary>
        public T CurrentState
        {
            get => _currentState;
            set
            {
                var oldState = default(T);
                if (_currentState != null)
                {
                    oldState = _currentState;
                    _statesMap[_currentState].OnStateExit();
                }

                _currentState = value;
                _statesMap[_currentState].OnStateEnter();
                OnStateChange?.Invoke(oldState, _currentState);
            }
        }

        /// <summary>
        /// Gets a boolean value indicating if the state machine is in a playing state.
        /// Set to true when the state machine is started with Start method.
        /// </summary>
        public bool IsPlay { get; private set; }

        /// <summary>
        /// Event that is triggered when the state of the object changes.
        /// The event handler should expect two parameters of type T representing the previous state and the new state.
        /// </summary>
        public event Action<T, T> OnStateChange;

        #endregion

        #region Public Methods

        /// <summary>
        /// Start the state machine, setting the initial state and triggering the start of all states.
        /// </summary>
        /// <param name="initState">The initial state to set for the state machine.</param>
        public void StartFsm(T initState = default)
        {
            if (initState == null)
            {
                initState = _statesMap.Keys.FirstOrDefault();
            }
            IsPlay = true;
            TriggerStartOnAllState();
            CurrentState = initState;
        }

        /// <summary>
        /// Transition to the specified state in the state machine.
        /// </summary>
        /// <param name="state">The state to transition to.</param>
        public void TransitionTo(T state)
        {
            CurrentState = state;
        }

        /// <summary>
        /// Add a new state to the state machine along with its corresponding state object.
        /// </summary>
        /// <param name="state">The state identifier to add along with its state object.</param>
        /// <param name="stateObject">The state object implementing the IState interface.</param>
        public void AddState(T state, IState stateObject)
        {
            if (!_statesMap.TryAdd(state, stateObject))
            {
                throw new ArgumentException($"State {state} already exists");
            }
        }

        /// <summary>
        /// Updates the current state in the state machine.
        /// If the state machine is not in the 'Play' state, the method returns without doing anything.
        /// Otherwise, it calls the OnStateUpdate method of the current state to perform state-specific update logic.
        /// </summary>
        public void Update()
        {
            if (!IsPlay) return;
            _statesMap[_currentState].OnStateUpdate();
        }

        /// <summary>
        /// Add a new state to the state machine along with its corresponding state object.
        /// Use this method if you don't have an instance of the state object and want to create it using the generic type.
        /// </summary>
        /// <param name="state">The state identifier to add along with its state object.</param>
        public void AddState<TState>(T state) where TState : IState<T>, new()
        {
            var instance = Activator.CreateInstance<TState>();
            instance.StateMachine = this;
            if (!_statesMap.TryAdd(state, instance))
            {
                throw new ArgumentException($"State {state} already exists");
            }
        }

        #endregion


        #region Private Methods

        private void TriggerStartOnAllState()
        {
            // _statesMap.Values.ForEach(state => state.Start());
            foreach (var state in _statesMap.Values.ToList())
            {
                state.OnStateStart();
            }
        }
        
        #endregion
    }
}