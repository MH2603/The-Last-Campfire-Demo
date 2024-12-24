using System.Collections.Generic;

namespace MH.Puzzle.RotatePuzzle
{
    public class RotatePuzzleManager : BasePuzzle
    {
        
        private RotateStatue[] _rotateStatues;
        private  Dictionary<int, RotateStatue> _statueMap = new ();

        protected override void Awake()
        {
            base.Awake();
            _rotateStatues = GetComponentsInChildren<RotateStatue>();

            for (int i = 0; i < _rotateStatues.Length; i++)
            {
                _rotateStatues[i].Initialize(this);
                _statueMap.Add(i, _rotateStatues[i]);
            }
        }

        #region  ------------- Public Methods -----------

        public void TryFinish()
        {
            for (int i = 0; i < _rotateStatues.Length; i++)
            {
                if(! _rotateStatues[i].IsOnDestinationDirection()) return;
            }

            FinishPuzzle();
        }

        public void LockRotateStatue(int statueIndexs)
        {
            for (int i = 0; i < _rotateStatues.Length; i++)
            {
                _rotateStatues[i].SetLockRotate(false);
            }
            
            if(! _statueMap.ContainsKey(statueIndexs)) return;
            _statueMap[statueIndexs].SetLockRotate(true);
        }

        #endregion
        
        
    }
}