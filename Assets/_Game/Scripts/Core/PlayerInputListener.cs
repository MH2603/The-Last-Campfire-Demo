using UnityEngine;
using UnityEngine.Events;

namespace MH
{
    public class PlayerInputListener : EntityComponent
    {
        [SerializeField] private KeyCode _keyCode;
        public UnityEvent OnKeyPressed = new UnityEvent();

        public override void ManualUpdate()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                OnKeyPressed?.Invoke();
            }
        }
        
    }
}