using UnityEngine;

namespace MH.InteractionCondition
{
    [CreateAssetMenu(
        fileName = "DistanceCondition",
        menuName = "SO/Interaction/Condition/Distance Condition",
        order = 0)] // 'order' determines where this option appears in the menu
    public class DistanceCondition : BaseInteractConditionConfig
    {
        [SerializeField] private float Distance;

        public override bool IsConditionMet(InteractionEventData eventData)
        {
            if ( Vector3.Distance(eventData.Player.transform.position, eventData.Interactable.transform.position) <= Distance )
            {
                return true;
            }
            
            return false;
        }


    }
}