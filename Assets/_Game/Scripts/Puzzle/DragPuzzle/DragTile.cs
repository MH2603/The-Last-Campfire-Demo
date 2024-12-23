using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MH.Puzzle.DragPuzzle
{
    public class DragTile : BaseEntity
    {
        #region -------- Inpspector -------------

        [SerializeField] private int _destinationSlotIndex;

        #endregion


        #region --------- Properties -------------

        private DragPuzzleBoard _board;
        private ClickableHandler _clickableHandler;

        private Transform _moveTransform;
        private Transform _startAnchor;
        private DragSlot _currentSlot;
        private Vector2 _dragStartPosition;
        private Vector3 targetPosition;
        private bool _isDragging;
        private bool _isMoving;
        private DragMovementConfig _movementConfig;

        private float moveSpeed => _movementConfig.MoveSpeed;
        private float dragThreshold => _movementConfig.DragThreshold;
        private float stopDistance => _movementConfig.StopDistance;

        #endregion

        #region ----------- Unity Methods ------------

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (_isMoving)
            {
                // Smoothly move towards the target position
                _moveTransform.position = Vector3.MoveTowards(_moveTransform.position, targetPosition,
                    moveSpeed * Time.fixedDeltaTime);

                // Stop moving when the target is reached
                if (Vector3.Distance(_moveTransform.position, targetPosition) < stopDistance)
                {
                    _isMoving = false;
                    _moveTransform.position = targetPosition;
                }
            }
        }

        #endregion

        #region ----------- Public Methods ------------

        public void Initialize(DragPuzzleBoard board)
        {
            _startAnchor = transform;

            _board = board;
            _movementConfig = board.DragMovementConfig;

            _clickableHandler = Get<ClickableHandler>();
            _clickableHandler.OnDownMouse += OnPointerDown;
            _clickableHandler.OnUpMouse += (eventData) => TrySnap();
            // _clickableHandler.OnExitMouse += (eventData) => TrySnap();
            _clickableHandler.OnDragEvent += OnPointerDrag;

            _moveTransform = _clickableHandler.transform;
        }


        public bool IsOnRightSlot()
        {
            return _currentSlot != null && _currentSlot.GridIndex == _destinationSlotIndex;
        }

        #endregion

        #region ------------ Private Methods ----------

        private void OnPointerDown(PointerEventData pointerEventData)
        {
            if (_isMoving
                || pointerEventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            _dragStartPosition = pointerEventData.position;
            _isDragging = true;
            
            if (_currentSlot != null)
            {
                _currentSlot.ExitTile();
                _currentSlot = null;
            }
        }

        private void OnPointerDrag(PointerEventData pointerEventData)
        {
            if (!_isDragging) return;

            Vector2 deltaDirection = pointerEventData.position - _dragStartPosition;
            _dragStartPosition = pointerEventData.position;

            TrySetTagretFollowMouse(deltaDirection);
        }


        private void TrySnap()
        {
            DragSlot[] slots = _board.GetSlots();
            DragSlot[] conditionSlots = slots.Where(slot => slot.IsEmpty() && 
                                                            Vector3.Distance(_moveTransform.position, slot.Position) <= _movementConfig.SnapDistance).
                                                            ToArray();
            if (conditionSlots.Length == 0)
            {
                SetTargetToStartAnchor();
                return;
            }

            DragSlot closetSlot = conditionSlots.OrderBy(slot => Vector3.Distance(_moveTransform.position, slot.Position))
                .First();
            closetSlot = conditionSlots[0];
            if (closetSlot == null)
            {
                SetTargetToStartAnchor();
                return;
            }

            targetPosition = closetSlot.Position;
            _isMoving = true;
            _currentSlot = closetSlot;
            closetSlot.TryEnterTile(this);
            
            _board.TryFinish();
        }

        private void SetTargetToStartAnchor()
        {
            // set taget == stat anchor
            targetPosition = _startAnchor.position;
            _isMoving = true;
        }

        private void TrySetTagretFollowMouse(Vector2 direction)
        {
            if (direction.magnitude < dragThreshold) return;

            _isMoving = true;

            // Calculate the local target position based on the direction
            Vector3 localDirection = new Vector3(direction.x, direction.y, 0);
            Vector3 worldDirection = _moveTransform.TransformDirection(localDirection);

            // Get world position of mouse
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(_moveTransform.position).z;
            Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Update the target position in world space
            targetPosition = _moveTransform.position + worldDirection;
            targetPosition = targetWorldPosition;

            // Flag that the object is moving
            _isMoving = true;
        }

        #endregion
    }
}