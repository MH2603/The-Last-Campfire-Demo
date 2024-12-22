using UnityEngine;

namespace MH.Puzzle.SlidingTile
{
    public class SlidingSlot : BaseEntity
    {
        #region ---------- Inpector ----------

        [SerializeField] private Vector2Int _gridPosition;
        [SerializeField] private Transform _tileAnchor;
        
        #endregion

        #region  ------------ Properties -------------
        
        private SlidingPuzzleBoard _board;
        private SlidingTile _currentTile;
    
        #endregion

        #region ----------- Public Methods --------

        public void Initialize(SlidingPuzzleBoard board)
        {
            _board = board;
        }
        
        public Vector2Int GetGridPos()
        {
            return _gridPosition;
        }
        
        public bool IsEmpty()
        {
            return _currentTile == null;
        }

        public void EnterTile(SlidingTile newTile)
        {
            if (!IsEmpty())
            {
                Debug.Log(" Sliding Slot: can not enter tile when have old tile !");
                return;
            }

            _currentTile = newTile;
        }

        public void ExitTile()
        {
            _currentTile = null;
        }

        public Vector3 GetTileAnchorPos()
        {
            return _tileAnchor ? _tileAnchor.position : transform.position;
        }

        #endregion
    }
}