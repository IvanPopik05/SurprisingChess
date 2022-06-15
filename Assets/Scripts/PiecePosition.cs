using DefaultNamespace.Pieces;
using UnityEngine;

namespace DefaultNamespace
{
    public class PiecePosition
    {
        private ChessPiece[,] _chessPieces;
        
        private readonly int _tileCountX;
        private readonly int _tileCountY;
        private readonly TileBoard _tileBoard;

        public PiecePosition(int tileCountX, int tileCountY, TileBoard tileBoard)
        {
            _tileCountX = tileCountX;
            _tileCountY = tileCountY;
            _tileBoard = tileBoard;
        }
        public void PositionAllPieces(ChessPiece[,] chessPieces)
        {
            _chessPieces = chessPieces;
            
            for (int x = 0; x < _tileCountX; x++)
            for (int y = 0; y < _tileCountY; y++)
                if (_chessPieces[x, y])
                    PositionSinglePiece(x, y, true);
        }

        public void PositionSinglePiece(int x, int y, bool force = false)
        {
            _chessPieces[x,y].SetCurrentPosition(x,y);
            _chessPieces[x,y].SetPosition(GetTileCenter(x,y),force);
        }
        
        private Vector3 GetTileCenter(int x, int y)
        {
            return new Vector3(x * _tileBoard.TileSize, _tileBoard.YOffset, y * _tileBoard.TileSize) - _tileBoard.Bounds 
                   + new Vector3(_tileBoard.TileSize/2,0,_tileBoard.TileSize/2);
        }
    }
}