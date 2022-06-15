using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Pieces
{
    public class Pawn : ChessPiece
    {
        public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int _tileCountX, int _tileCountY)
        {
            List<Vector2Int> r = new List<Vector2Int>();

            int direction = _team == 0 ? 1 : -1;
            
            if(!board[_currentX, _currentY + direction])
            {
                r.Add(new Vector2Int(_currentX,_currentY + direction));
            }

            if (!board[_currentX, _currentY + direction * 2] && _currentY == 1)
            {
                r.Add(new Vector2Int(_currentX,_currentY + direction * 2));
            }
            if (!board[_currentX, _currentY + direction * 2] && _currentY == 6)
            {
                r.Add(new Vector2Int(_currentX,_currentY + direction * 2));
            }

            if (_currentX != _tileCountX - 1)
            {
                if (board[_currentX + 1, _currentY + direction]
                    && board[_currentX + 1, _currentY + direction].Team != _team)
                {
                    r.Add(new Vector2Int(_currentX + 1, _currentY + direction));
                }
            }

            if (_currentX != 0)
            {
                if (board[_currentX - 1, _currentY + direction]
                    && board[_currentX -1, _currentY + direction].Team != _team)
                {
                    r.Add(new Vector2Int(_currentX - 1, _currentY + direction));
                }
            }

            return r;
        }
    }
}