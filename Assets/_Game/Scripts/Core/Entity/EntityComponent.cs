using UnityEngine;

namespace MH
{
    public abstract class EntityComponent: MonoBehaviour
    {
        
        protected BaseEntity baseEntity;   
        
        #region ------- Public Methods -----------

        public virtual void SetUp(BaseEntity baseEntitys)
        {
            baseEntity = baseEntitys;
        }
        public virtual void ManualStart(){}
        public virtual void ManualUpdate(){}
        public virtual void ManualFixedUpdate(){}
       
        #endregion
    }
}