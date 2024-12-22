using System.Collections.Generic;
using UnityEngine;

namespace MH.Puzzle.SlidingTile
{
    public class SlidingPuzzleBoard : BasePuzzle
    {
        [SerializeField] private int gridWidth = 3;
        [SerializeField] private int gridHeight = 3;
        [SerializeField] private TileMovementConfig movementConfig = TileMovementConfig.Default;

        private Dictionary<Vector2Int, SlidingSlot> _slotMap = new();
        private List<SlidingTile> _tiles = new();
        private Vector2Int _emptySlotPosition;

        protected override void Awake()
        {
            base.Awake();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            // Initialize slots
            SlidingSlot[] slots = GetComponentsInChildren<SlidingSlot>();
            foreach (SlidingSlot slot in slots)
            {
                Vector2Int pos = slot.GetGridPos();
                _slotMap[pos] = slot;
                slot.RegisterBoard(this);
            }

            // Initialize tiles
            _tiles.AddRange(GetComponentsInChildren<SlidingTile>());
            for (int i = 0; i < _tiles.Count; i++)
            {
                _tiles[i].RegisterBoard(this);
                _tiles[i].Initialize(i);
            }

            // Find empty slot
            foreach (var kvp in _slotMap)
            {
                if (kvp.Value.IsEmpty())
                {
                    _emptySlotPosition = kvp.Key;
                    break;
                }
            }
        }

        public SlidingSlot GetSlot(Vector2Int gridPos)
        {
            return _slotMap.TryGetValue(gridPos, out var slot) ? slot : null;
        }

        public void CheckWinCondition()
        {
            bool isWin = true;
            foreach (SlidingTile tile in _tiles)
            {
                // Add your win condition logic here
                // For example, check if each tile is in its correct position
            }

            if (isWin)
            {
                OnPuzzleComplete();
            }
        }

        protected virtual void OnPuzzleComplete()
        {
            // Implement puzzle completion logic
            Debug.Log("Puzzle Completed!");
        }
    }
}