
using MH.Component;
using UnityEngine;
using UnityEngine.Events;

namespace MH.Puzzle.SlidingTile
{
    public class SlidingPuzzleInteractionHandler : BaseEntity, IInteractable
    {
        #region ---------- Inpectors ------------

        [SerializeField] private SlidingPuzzleBoard _slidingPuzzleBoard;
        [SerializeField] private float _conditionDistance = 0.5f;
        // [SerializeField] private Transform _playerStandPos;
        
        [Space]
        [SerializeField] private string _cinemachineName;
        
        [Space]
        [SerializeField] private UnityEvent OnInteracted;

        #endregion
        
        
        #region ---------- Properties ------------

        private ClickableHandler _clickableHandler;
        private PlayerServiceProvider _playerService => ServiceLocator.Instance.GetService<PlayerServiceProvider>();
        private ICinemachineService _cinemachineService => ServiceLocator.Instance.GetService<ICinemachineService>();
        private Transform _transform;
            
        
        #endregion

        #region  ----------- Unity Methods --------------

        protected override void Start()
        {
            base.Start();
            
            _transform = transform;
            
            _clickableHandler = Get<ClickableHandler>();
            _clickableHandler.OnDownMouse += (eventData) => Interact();

            _slidingPuzzleBoard.OnFinished += OnFinished;
        }

        #endregion
        

        #region  ----------- Public Methods -----------

        public bool CanInteract()
        {
            if ( Vector3.Distance(_transform.position, _playerService.PlayerTransform.position) <= _conditionDistance )
            {
                return true;
            }
            
            return false;
        }

        public void Interact()
        {
            if(!CanInteract()) return;
            
            _playerService.characterNavMovement.Stop();
            _clickableHandler.gameObject.SetActive(false);
            _cinemachineService.UseCinemachine(_cinemachineName);
            
            OnInteracted?.Invoke();
        }

        public void OnFinished()
        {
            _cinemachineService.UseDefaultCinemachine();
            _clickableHandler.gameObject.SetActive(true);
        }

        #endregion

        
        
    }
}