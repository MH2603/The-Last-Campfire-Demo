using System.Threading.Tasks;
using DG.Tweening;
using MH.Extentions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MH.Puzzle.SlidingTile
{
    // Enum to track tile movement direction
    public enum MoveDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    // Data structure to hold tile movement configuration
    [System.Serializable]
    public struct TileMovementConfig
    {
        public float moveSpeed;
        public float moveTime;
        public float dragThreshold;
        public float snapDistance;

        public static TileMovementConfig Default => new TileMovementConfig
        {
            moveSpeed = 8f,
            moveTime = 0.5f,
            dragThreshold = 0.1f,
            snapDistance = 0.5f
        };
    }

    public class SlidingTile : BaseEntity
    {
        [SerializeField] private Vector2Int currentGridPosition;
        [SerializeField] private Vector2Int completeGridPos;

        private SlidingPuzzleBoard _board;
        private ClickableHandler _clickableHandler;
        
        private Vector2 _dragStartPosition;
        private Vector2Int _tileStartPosition;
        private bool _isDragging;
        private bool _isMoving;

        private TileMovementConfig _movementConfig = TileMovementConfig.Default;

        protected override void Awake()
        {
            base.Awake();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _clickableHandler = Get<ClickableHandler>();
            _clickableHandler.OnDownMouse += OnPointerDown;
            _clickableHandler.OnExitMouse += OnPointerExit;
        }
        
        public void Initialize(SlidingPuzzleBoard board)
        {
            _board = board;

            transform.position = _board.GetSlot(currentGridPosition).GetTileAnchorPos();
            _movementConfig = _board.TileMovementConfig;
            
            
        }

        public bool IsOnCompletePos()
        {
            return currentGridPosition == completeGridPos;
        }
            

        private void OnPointerDown(PointerEventData eventData)
        {
            if (_isMoving || _board.State != SlidingPuzzleBoard.BoardState.Playing) return;

            _dragStartPosition = eventData.position;
            _tileStartPosition = currentGridPosition;
            _isDragging = true;
        }

        private void OnPointerExit(PointerEventData eventData)
        {
            if(!_isDragging) return;

            _isDragging = false;
            Vector2 dragDirection = eventData.position - _dragStartPosition;

            TryMove(GetClosestMoveDirection(dragDirection));
        }

        private MoveDirection GetClosestMoveDirection(Vector2 dragDelta)
        {
            if (dragDelta.magnitude < _movementConfig.dragThreshold)
                return MoveDirection.None;

            float angle = Mathf.Atan2(dragDelta.y, dragDelta.x) * Mathf.Rad2Deg;

            if (angle > -45 && angle <= 45) return MoveDirection.Right;
            if (angle > 45 && angle <= 135) return MoveDirection.Up;
            if (angle > 135 || angle <= -135) return MoveDirection.Left;
            return MoveDirection.Down;
        }

        private bool TryMove(MoveDirection direction)
        {
            if (direction == MoveDirection.None)
                return false;

            Vector2Int targetPos = GetTargetPosition(direction);
            SlidingSlot targetSlot = _board.GetSlot(targetPos);

            if (targetSlot == null || !targetSlot.IsEmpty())
                return false;

            StartMove(targetSlot);
            return true;
        }

        private Vector2Int GetTargetPosition(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up: return currentGridPosition + Vector2Int.up;
                case MoveDirection.Down: return currentGridPosition + Vector2Int.down;
                case MoveDirection.Left: return currentGridPosition + Vector2Int.left;
                case MoveDirection.Right: return currentGridPosition + Vector2Int.right;
                default: return currentGridPosition;
            }
        }

        private async void StartMove(SlidingSlot targetSlot)
        {
            _isMoving = true;
            SlidingSlot currentSlot = _board.GetSlot(currentGridPosition);

            if (currentSlot != null)
                currentSlot.ExitTile();

            currentGridPosition = targetSlot.GetGridPos();
            targetSlot.EnterTile(this);

            // Animate movement using Unity's animation system or coroutine
            // For simplicity, just updating position instantly here
            // transform.position = targetSlot.GetTileAnchorPos();
            transform.DOKill();
            transform.DOMove(targetSlot.GetTileAnchorPos(), _movementConfig.moveTime).SetEase(Ease.Linear);

            // wait for a time 
            await Task.Delay( FloatExtentions.ToMiniSeconds(_movementConfig.moveTime) );

            transform.position = targetSlot.GetTileAnchorPos();
            _isMoving = false;
            
            
            _board.CheckWinCondition();
        }
    }
}