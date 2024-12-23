using MH.Component;
using MH.Puzzle.SlidingTile;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace MH
{
    public class InteractableEntity : BaseEntity, IInteractable
    {

        #region --------- Inpspectors -----------
        [SerializeField] private InteractionConfig _interactionConfig;
        public UnityEvent OnInteracted = new UnityEvent();
        #endregion

        #region ------------ Properties  ----------

        private ObjectPool<InteractionEventData> _eventDataPool;
        private PlayerServiceProvider _playerServiceProvider;
        private ClickableHandler _clickableHandler;
        
        private InteractionEventData _interactionEventData = new InteractionEventData();

        #endregion

        protected override void Start()
        {
            base.Start();
            InitPool();

            if (_interactionConfig.EInteractType == EInteractType.Click)
            {
                _clickableHandler = Get<ClickableHandler>();
                _clickableHandler.OnDownMouse += (PointerEventData) => Interact();
            }
        }


        public bool CanInteract()
        {
            if (_playerServiceProvider == null)
            {
                _playerServiceProvider = ServiceLocator.Instance.GetService<PlayerServiceProvider>();
            }
            
            _interactionEventData.Player = _playerServiceProvider.PlayerEntity;
            _interactionEventData.Interactable = this;

            return _interactionConfig.AreAllConditionsMet(_interactionEventData);
        }

        public void Interact()
        {
            if(!CanInteract()) return;
            
            OnInteracted?.Invoke();
        }


        private void InitPool()
        {
            _eventDataPool = new ObjectPool<InteractionEventData>(
                createFunc: () => new InteractionEventData(), // Function to create a new instance
                // actionOnGet: eventData => eventData.Reset(), // Called when an object is taken from the pool
                // actionOnRelease: eventData => eventData.Clear(), // Called when an object is returned to the pool
                actionOnDestroy: eventData => Debug.Log("Object Destroyed"), // Optional, for cleanup if needed
                collectionCheck: true, // Enable collection check for debugging purposes
                defaultCapacity: 10, // Initial capacity of the pool
                maxSize: 50 // Maximum size of the pool
            );
        }
        
    }
}