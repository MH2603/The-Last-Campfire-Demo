namespace MH.StateMachine
{
    public enum CharacterState
    {
       
        Locamotion,
        Cinematic // 
    }
    
    public class PlayerStateMachine : StateMachine<CharacterState>
    {

        public PlayerStateMachine(BaseEntity baseEntity)
        {
            AddState(CharacterState.Locamotion, new CharacterLocomotionState(baseEntity, this ));
            
            StartFsm(CharacterState.Locamotion);
        }
    }
}