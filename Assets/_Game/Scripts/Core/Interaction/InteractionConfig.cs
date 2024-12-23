
using UnityEngine;

namespace MH
{
    public enum EInteractType
    {
        Click,
        TriggerCollider
    }
    
    public abstract class BaseInteractConditionConfig : ScriptableObject
    {
        public virtual bool IsConditionMet(InteractionEventData interactionEventData)
        {   
            return false;
        }
    }
    
    
    [CreateAssetMenu(
        fileName = "InteractionConfig",
        menuName = "SO/Interaction/InteractionConfig",
        order = 0)] // 'order' determines where this option appears in the menu
    public class InteractionConfig : ScriptableObject
    {
        [field: SerializeField] public EInteractType EInteractType { get; private set; }
        [field: SerializeField] public BaseInteractConditionConfig[] Conditions;


        public bool AreAllConditionsMet(InteractionEventData interactionEventData)
        {
            for (int i=0; i<Conditions.Length; i++)
            {
                if (!Conditions[i].IsConditionMet(interactionEventData)) return false;
            }
            
            return true;
        }
    }
}