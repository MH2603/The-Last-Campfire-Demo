using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace MH.Puzzle.SlidingTile
{
    public class SlidingPuzzleBoard : BaseEntity
    {
        public enum BoardState
        {
            Empty,
            Playing,
            Finished,
        }
        
        #region --------- Inspectors ----------s

        [SerializeField] private TileMovementConfig movementConfig = TileMovementConfig.Default;
        public UnityEvent OnEnter;
        public UnityEvent OnFinished;
        public UnityEvent OnExit;
        #endregion

        #region ------------ Properties ----------

        private Dictionary<Vector2Int, SlidingSlot> _slotMap = new();
        private List<SlidingTile> _tiles = new();

        private BoardState _boardState = BoardState.Empty;
        
        public TileMovementConfig TileMovementConfig => movementConfig;
        public BoardState State => _boardState;
        
        
        #endregion

        #region ---------- Unity Methods -------------

        protected override void Awake()
        {
            base.Awake();
            InitializeBoard();
        }

        #endregion

                

        private void InitializeBoard()
        {
            // Initialize slots
            SlidingSlot[] slots = GetComponentsInChildren<SlidingSlot>();
            foreach (SlidingSlot slot in slots)
            {
                Vector2Int pos = slot.GetGridPos();
                _slotMap[pos] = slot;
                slot.Initialize(this);
            }

            // Initialize tiles
            _tiles = GetComponentsInChildren<SlidingTile>().ToList();
            for (int i = 0; i < _tiles.Count; i++)
            {
                _tiles[i].Initialize(this);
            }

            
        }

        public SlidingSlot GetSlot(Vector2Int gridPos)
        {
            return _slotMap.TryGetValue(gridPos, out var slot) ? slot : null;
        }

        public void EnterPuzzle()
        {
            _boardState = BoardState.Playing;
            
            OnEnter?.Invoke();
        }

        public void ExitPuzzle()
        {
            if( _boardState == BoardState.Playing)
            {
                _boardState = BoardState.Empty;
            }
            
            OnExit?.Invoke();
        }
        
        public void CheckWinCondition()
        {
            bool isWin = true;
            foreach (SlidingTile tile in _tiles)
            {
                // Add your win condition logic here
                // For example, check if each tile is in its correct position
                if (tile.IsOnCompletePos() == false)
                {
                    isWin = false;
                    return;
                }
            }

            if (isWin)
            {
                OnPuzzleComplete();
            }
        }

        protected virtual void OnPuzzleComplete()
        {
            _boardState = BoardState.Finished;
            
            // Implement puzzle completion logic
            Debug.Log("Puzzle Completed!");
            
            OnFinished?.Invoke();
        }
    }
}