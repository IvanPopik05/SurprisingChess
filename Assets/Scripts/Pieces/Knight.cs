using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Pieces
{
    public class Knight : ChessPiece
    {
        public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int _tileCountX, int _tileCountY)
        {
            List<Vector2Int> r = new List<Vector2Int>();

            // Top Right
            int x = _currentX + 1;
            int y = _currentY + 2;

            if (x < _tileCountX && y < _tileCountY)
            {
                if (!board[x, y] || board[x, y].Team != _team) 
                    r.Add(new Vector2Int(x, y));
            }

            x = _currentX + 2;
            y = _currentY + 1;

            if (x < _tileCountX && y < _tileCountY)
            {
                if (!board[x, y] || board[x, y].Team != _team)
                    r.Add(new Vector2Int(x,y));   
            }
            
            
            // Top Left
            x = _currentX - 1;
            y = _currentY + 2;

            if (x >= 0 && y < _tileCountY)
            {
                if (!board[x, y] || board[x, y].Team != _team)
                    r.Add(new Vector2Int(x,y));   
            }
            
            x = _currentX - 2;
            y = _currentY + 1;

            if (x >= 0 && y < _tileCountY)
            {
                if (!board[x, y] || board[x, y].Team != _team)
                    r.Add(new Vector2Int(x,y));   
            }
            
            // Bottom Right
            x = _currentX + 2;
            y = _currentY -1;

            if (x < _tileCountX && y >= 0)
            {
                if (!board[x, y] || board[x, y].Team != _team)
                    r.Add(new Vector2Int(x,y));   
            }
            
            x = _currentX + 1;
            y = _currentY - 2;

            if (x < _tileCountX && y >= 0)
            {
                if (!board[x, y] || board[x, y].Team != _team)
                    r.Add(new Vector2Int(x,y));   
            }
            
            // Bottom Left
            x = _currentX - 2;
            y = _currentY - 1;
            
            if (x >= 0 && y >= 0)
            {
                if (!board[x, y] || board[x, y].Team != _team)
                    r.Add(new Vector2Int(x,y));   
            }
            
            x = _currentX - 1;
            y = _currentY - 2;
            
            if (x >= 0 && y >= 0)
            {
                if (!board[x, y] || board[x, y].Team != _team)
                    r.Add(new Vector2Int(x,y));   
            }

            return r;
        }
    }
}