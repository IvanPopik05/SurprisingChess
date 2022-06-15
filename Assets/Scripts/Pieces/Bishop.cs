using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace DefaultNamespace.Pieces
{
    public class Bishop : ChessPiece
    {
        public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int _tileCountX, int _tileCountY)
        {
            List<Vector2Int> r = new List<Vector2Int>();
                
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
                        break;
                    }
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
                        break;
                    }
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
                        break;
                    }
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
                        break;
                    }
                }
            }
            return r;
        }
    }
}