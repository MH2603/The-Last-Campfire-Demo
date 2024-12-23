using System;
using UnityEngine;

namespace MH.Puzzle.DragPuzzle
{
    public class DragSlot : BaseEntity
    {
        #region ---------- Inspectors ---------

        [SerializeField] private int _gridIndex;
        [SerializeField] private Transform _tileAnchor;
        #endregion

        private DragTile _dragTile;
        
        public int GridIndex => _gridIndex;

        public Vector3 Position => _tileAnchor.position;
        
        public bool IsEmpty()
        {
            return _dragTile == null;
        }

        public void TryEnterTile(DragTile tile)
        {
            if(!IsEmpty()) return;   
            
            _dragTile = tile;
        }

        public void ExitTile()
        {
            _dragTile = null;
        }

        public Vector3 TilePosition()
        {
            return _tileAnchor? _tileAnchor.position : transform.position;
        }
    }
}