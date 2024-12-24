using Cysharp.Threading.Tasks;
using DG.Tweening;
using MH.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace MH.Puzzle.RotatePuzzle
{
    public enum EDirection
    {
        Right = 0,
        Left = 1,
        Back = 2,
        Forward = 3,
    }
    
    
    public class RotateStatue : BaseEntity
    {
        #region  -------------- Inspectors --------------

        [SerializeField] private int _index;
        [SerializeField] private EDirection _destiantionDirection;
        [SerializeField] private EDirection _startDirection;
        
        [Space]
        [SerializeField] private Transform _rotateTransform;
        [SerializeField] private Ease _rotateEase = Ease.OutBack;
        [SerializeField] private float _rotateDuration = 100f;

        [Space] 
        public UnityEvent OnLock;
        public UnityEvent OnUnLock;
        #endregion

        #region  ----------- Properties -----------

        private RotatePuzzleManager _rotatePuzzleManager;
        
        private EDirection _currentDirection;
        private bool _isRotating;
        private bool _isLocking;
        
        public int Index => _index;

        #endregion

        #region  -------- Entity Methods -------------

        

        #endregion

        #region ----------- Public Methods -------------

        public void Initialize(RotatePuzzleManager manager)
        {
            _rotatePuzzleManager = manager;
            
            _rotateTransform.localEulerAngles = ConvertDirectionToAngle(_startDirection);
            _currentDirection = _startDirection;
        }

        public void SetLockRotate(bool isLocked)
        {
            _isLocking = isLocked;
            if (isLocked)
            {
                OnLock?.Invoke();
            }
            else
            {
                OnUnLock?.Invoke();
            }
        }

        public async void TryRotate()
        {
            if (_isLocking || _isRotating || _rotatePuzzleManager.CurrentState != EPuzzleState.Playing) return;
            
            _isRotating = true;
            
            EDirection nextDirection = GetNextDirection(_currentDirection);

            _rotateTransform.DOKill();
            _rotateTransform.DOLocalRotate(ConvertDirectionToAngle(nextDirection), _rotateDuration);
            
            
            await UniTask.Delay(FloatExtentions.ToMiniSeconds(_rotateDuration + 0.1f));
            
            _rotateTransform.localEulerAngles = ConvertDirectionToAngle(nextDirection);
            _currentDirection = nextDirection;
            _isRotating = false;
            
            _rotatePuzzleManager.TryFinish();
        }

        public bool IsOnDestinationDirection()
        {
            return _currentDirection == _destiantionDirection;
        }
        
        #endregion


        #region  ------------ Private Methods ---------

        private Vector3 ConvertDirectionToAngle(EDirection direction)
        {
            Vector3 angle = new Vector3(0, 0, 0);
            switch (direction)
            {
                case EDirection.Right:
                    angle.y = 90;
                    break;
                case EDirection.Left:
                    angle.y = -90;
                    break;
                case EDirection.Back:
                    angle.y = 180;
                    break;
                
            }

            return angle;
        }

        private EDirection GetNextDirection(EDirection direction)
        {
            switch (direction)
            {
                case EDirection.Right:
                    return EDirection.Back;
                case EDirection.Left:
                    return EDirection.Forward;
                case EDirection.Back:
                    return EDirection.Left;
                
                
            }

            return EDirection.Right;
        }

        #endregion
    }
}