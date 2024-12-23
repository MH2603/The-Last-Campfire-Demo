using MH;
using MH.Component;
using UnityEngine;

namespace MH.Command
{
    public class MovePlayerCommand : BaseCommand
    {
        [SerializeField] private Transform _destination;

        private CharacterNavMovement _movementCtrl;
        
        public override void Execute()
        {
            if (_movementCtrl == null)
            {
                _movementCtrl = ServiceLocator.Instance.GetService<PlayerServiceProvider>().CharacterNavMovement;
            }

            if (_movementCtrl == null) return;
            
            _movementCtrl.MoveTo(_destination.position);
        }

    }
}