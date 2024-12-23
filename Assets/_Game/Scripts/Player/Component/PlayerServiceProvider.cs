using UnityEngine;
using UnityEngine.Serialization;

namespace MH.Component
{
    public class PlayerServiceProvider : EntityComponent, IGameService
    {
        
        [FormerlySerializedAs("characterNavMovement")] [HideInInspector] public CharacterNavMovement CharacterNavMovement;
        [FormerlySerializedAs("PlayerRoot")] [HideInInspector] public Transform PlayerTransform;
        [HideInInspector] public PlayerEntity PlayerEntity => (PlayerEntity)baseEntity;
        
        public override void ManualStart()
        {
            PlayerTransform = baseEntity.transform;
            CharacterNavMovement = baseEntity.Get<CharacterNavMovement>();
            
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