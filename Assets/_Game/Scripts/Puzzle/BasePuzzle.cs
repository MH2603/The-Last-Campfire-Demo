using MH;
using UnityEngine.Events;


namespace  MH.Puzzle
{
    public abstract class BasePuzzle : BaseEntity
    {
        public UnityEvent OnEnter;
        public UnityEvent OnComplete;
        public UnityEvent OnExit;
    }
}

