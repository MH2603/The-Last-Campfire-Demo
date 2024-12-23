using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace MH.Puzzle.DragPuzzle
{
    [System.Serializable]
    public struct DragMovementConfig
    {
        public float MoveSpeed;
        public float DragThreshold;
        public float StopDistance;
        public float SnapDistance;

        public static DragMovementConfig Default => new DragMovementConfig()
        {
            MoveSpeed = 1,
            DragThreshold = 0.01f,
            StopDistance = 0.01f,
            SnapDistance = 0.5f
        };
    }

    public enum EBoardState
    {
        Empty,
        Playing,
        Finished,
    }
    
    public class DragPuzzleBoard : BaseEntity
    {
        #region ---------- Inpsectors ------------

        [field: SerializeField] public DragMovementConfig DragMovementConfig { get; private set; }
        public UnityEvent OnEnter;
        public UnityEvent OnExit;
        public UnityEvent OnFinished;

        public EBoardState CurrentState => _boardState;
        #endregion
        
        private EBoardState _boardState = EBoardState.Empty;
        
        #region ---------- Properties ------------

        private DragTile[] _tiles;
        private Dictionary<int, DragSlot> _slotMap = new();

        #endregion


        #region ---------- Untiy Methods --------

        protected override void Start()
        {
            base.Start();
            DragSlot[] dragSlots = GetComponentsInChildren<DragSlot>();
            foreach (var slot in dragSlots)
            {
                _slotMap.Add(slot.GridIndex, slot);
            }
            
            _tiles = GetComponentsInChildren<DragTile>();
            for (int i=0; i<_tiles.Length; i++)
            {
                _tiles[i].Initialize(this);
            }
            
        }

        #endregion

        #region ---------- Public Methods -----------

        public DragSlot[] GetSlots()
        {
            return _slotMap.Values.ToArray();
        }

        public void TryFinish()
        {
            for (int i=0; i<_tiles.Length; i++)
            {
                if(!_tiles[i].IsOnRightSlot()) return;
            }
            
            _boardState = EBoardState.Finished;
            OnFinished?.Invoke();
        }

        public void EnterPuzzle()
        {
            if(_boardState == EBoardState.Empty) _boardState = EBoardState.Playing;
            
            OnEnter?.Invoke();
        }

        public void ExitPuzzle()
        {
            if(_boardState == EBoardState.Playing ) _boardState = EBoardState.Empty;
            
            OnExit?.Invoke();
        }

        #endregion
    }
}