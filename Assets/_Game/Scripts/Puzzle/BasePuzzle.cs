using MH;
using UnityEngine.Events;


namespace  MH.Puzzle
{

    public enum EPuzzleState
    {
        Empty = 0,
        Playing = 1,
        Finished = 2,
    }
    
    public abstract class BasePuzzle : BaseEntity
    {
        public UnityEvent OnEnter;
        public UnityEvent OnFinish;
        public UnityEvent OnExit;

        public EPuzzleState CurrentState => currentState;
        
        protected EPuzzleState currentState = EPuzzleState.Empty;
        
        
        public virtual void EnterPuzzle()
        {
            if (currentState == EPuzzleState.Empty)
            {
                currentState = EPuzzleState.Playing;
            }
            
            OnEnter?.Invoke();
        }

        public virtual void ExitPuzzle()
        {
            if ( currentState == EPuzzleState.Playing )
            {
                currentState = EPuzzleState.Empty;
            }
            
            
            OnExit?.Invoke();
        }

        protected virtual void FinishPuzzle()
        {
            currentState = EPuzzleState.Finished;
            OnFinish?.Invoke();
        }
    }
}

