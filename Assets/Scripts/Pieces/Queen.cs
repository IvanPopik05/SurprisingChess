using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Pieces
{
    public class Queen : ChessPiece
    {
        public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int _tileCountX, int _tileCountY)
        {
            List<Vector2Int> r = new List<Vector2Int>();
                
             //Up
            for (int i = _currentY + 1; i < _tileCountY; i++)
            {
                if (!board[_currentX, i])
                {
                    r.Add(new Vector2Int(_currentX,i));
                }

                if (board[_currentX, i])
                {
                    if (board[_currentX, i].Team != _team)
                    {
                        r.Add(new Vector2Int(_currentX,i));
                    }   
                    break;
                }
            }
            
            //Down
            for (int i = _currentY - 1; i >= 0; i--)
            {
                if (!board[_currentX, i])
                {
                    r.Add(new Vector2Int(_currentX,i));
                }

                if (board[_currentX, i])
                {
                    if (board[_currentX, i].Team != _team)
                    {
                        r.Add(new Vector2Int(_currentX,i));
                    }   
                    break;
                }
            }
            
            //Left
            for (int i = CurrentX - 1; i >= 0; i--)
            {
                if (!board[i, _currentY])
                {
                    r.Add(new Vector2Int(i,_currentY));
                }

                if (board[i, _currentY])
                {
                    if (board[i, _currentY].Team != _team)
                    {
                        r.Add(new Vector2Int(i,_currentY));
                    }   
                    break;
                }
            }
            
            
            //Right
            for (int i = CurrentX + 1; i < _tileCountX; i++)
            {
                if (!board[i, _currentY])
                {
                    r.Add(new Vector2Int(i,_currentY));
                }

                if (board[i, _currentY])
                {
                    if (board[i, _currentY].Team != _team)
                    {
                        r.Add(new Vector2Int(i,_currentY));
                    }   
                    break;
                }
            }
            
            //Up Right
            for (int x = CurrentX + 1, y = CurrentY + 1;  x < _tileCountX && y < _tileCountY; x++,y++)
            {
                if (!board[x, y])
                {
                    r.Add(new Vector2Int(x,y));
                }

                if (board[x, y])
                {
                    if (board[x, y].Team != _team)
                    {
                        r.Add(new Vector2Int(x,y));
                    }
                    break;
                }
            }
            
            //Up Left
            for (int x = CurrentX - 1, y = CurrentY + 1;  x >= 0 && y < _tileCountY; x--,y++)
            {
                if (!board[x, y])
                {
                    r.Add(new Vector2Int(x,y));
                }

                if (board[x, y])
                {
                    if (board[x, y].Team != _team)
                    {
                        r.Add(new Vector2Int(x,y));
                    }
                    break;
                }
            }
            
            //Bottom Left
            for (int x = CurrentX - 1, y = CurrentY - 1;  x >= 0 && y >= 0; x--,y--)
            {
                if (!board[x, y])
                {
                    r.Add(new Vector2Int(x,y));
                }

                if (board[x, y])
                {
                    if (board[x, y].Team != _team)
                    {
                        r.Add(new Vector2Int(x,y));
                    }
                    break;
                }
            }
            
            //Bottom Right
            for (int x = CurrentX + 1, y = CurrentY - 1;  x < _tileCountX && y >= 0; x++,y--)
            {
                if (!board[x, y])
                {
                    r.Add(new Vector2Int(x,y));
                }

                if (board[x, y])
                {
                    if (board[x, y].Team != _team)
                    {
                        r.Add(new Vector2Int(x,y));
                    }
                    break;
                }
            }
            return r;
        }
    }
}