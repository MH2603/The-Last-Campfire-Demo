using MH.Component;
using UnityEngine;

namespace MH.Command
{
    public class SetLockMovementCommand : BaseCommand
    {
        [SerializeField] private bool _lockMovement;
        private CharacterNavMovement _movementCtrl;
        public override void Execute()
        {
            if (_movementCtrl == null)
            {
                _movementCtrl = ServiceLocator.Instance.GetService<PlayerServiceProvider>().CharacterNavMovement;
            }

            if (_movementCtrl == null) return;
            
            _movementCtrl.SetLock(_lockMovement);
        }
    }
}