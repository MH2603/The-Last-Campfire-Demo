namespace MH
{
    // Interface for anything that can be interacted with by the player
    public interface IInteractable
    {
        public bool CanInteract();
        public void Interact();
    }
}