using UnityEngine;
using UnityEngine.Serialization;

namespace MH.Component
{
    public class PlayerServiceProvider : EntityComponent, IGameService
    {
        
        [HideInInspector] public CharacterNavMovement characterNavMovement;
        [FormerlySerializedAs("PlayerRoot")] [HideInInspector] public Transform PlayerTransform;
        public override void ManualStart()
        {
            PlayerTransform = baseEntity.transform;
            characterNavMovement = baseEntity.Get<CharacterNavMovement>();
            
            ServiceLocator.Instance.RegisterService(this);
        }
        
        public void Initialize()
        {
            
        }

        public void Cleanup()
        {
            
        }
    }
}