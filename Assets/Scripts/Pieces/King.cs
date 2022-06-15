using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Pieces
{
    public class King : ChessPiece
    {
        public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int _tileCountX, int _tileCountY)
        {
            List<Vector2Int> r = new List<Vector2Int>();
            // Right
            
            // right
            if (_currentX + 1 < _tileCountX)
            {
                if (!board[_currentX + 1, _currentY])
                    r.Add(new Vector2Int(_currentX + 1, _currentY));
                else if (board[_currentX + 1,_currentY].Team != _team) 
                    r.Add(new Vector2Int(_currentX + 1, _currentY));
            }
            
            // right up
            if (_currentX + 1 < _tileCountX && _currentY + 1 < _tileCountY)
            {
                if (!board[_currentX + 1, _currentY + 1])
                    r.Add(new Vector2Int(_currentX + 1, _currentY + 1));
                else if (board[_currentX + 1,_currentY + 1].Team != _team) 
                    r.Add(new Vector2Int(_currentX + 1, _currentY + 1));
            }
            
            // right down
            if (_currentX + 1 < _tileCountX && _currentY - 1 >= 0)
            {
                if (!board[_currentX + 1, _currentY - 1])
                    r.Add(new Vector2Int(_currentX + 1, _currentY - 1));
                else if (board[_currentX + 1,_currentY - 1].Team != _team) 
                    r.Add(new Vector2Int(_currentX + 1, _currentY - 1));
            }

            // Left
            
            // left
            if (_currentX - 1 >= 0)
            {
                if (!board[_currentX - 1, _currentY])
                    r.Add(new Vector2Int(_currentX - 1, _currentY));
                else if (board[_currentX - 1,_currentY].Team != _team) 
                    r.Add(new Vector2Int(_currentX - 1, _currentY));
            }
            // left up
            if (_currentX - 1 >= 0 && _currentY + 1 < _tileCountY)
            {
                if (!board[_currentX - 1, _currentY + 1])
                    r.Add(new Vector2Int(_currentX - 1, _currentY + 1));
                else if (board[_currentX - 1,_currentY + 1].Team != _team) 
                    r.Add(new Vector2Int(_currentX - 1, _currentY + 1));
            }
            
            // left down
            if (_currentX - 1 >= 0 && _currentY - 1 >= 0)
            {
                if (!board[_currentX - 1, _currentY - 1])
                    r.Add(new Vector2Int(_currentX - 1, _currentY - 1));
                else if (board[_currentX - 1,_currentY - 1].Team != _team) 
                    r.Add(new Vector2Int(_currentX - 1, _currentY - 1));
            }
            // Up
            if (_currentY + 1 < _tileCountY)
            {
                if (!board[_currentX, _currentY + 1])
                    r.Add(new Vector2Int(_currentX, _currentY + 1));
                else if (board[_currentX,_currentY + 1].Team != _team) 
                    r.Add(new Vector2Int(_currentX, _currentY + 1));
            }
            // Bottom
            if (_currentY - 1 >= 0)
            {
                if (!board[_currentX, _currentY - 1])
                    r.Add(new Vector2Int(_currentX, _currentY - 1));
                else if (board[_currentX,_currentY - 1].Team != _team) 
                    r.Add(new Vector2Int(_currentX, _currentY - 1));
            }
            return r;
        }
    }
}