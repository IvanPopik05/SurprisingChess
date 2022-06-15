using DefaultNamespace.Pieces;
using UnityEngine;

namespace DefaultNamespace
{
    public class SpawnerPieces : MonoBehaviour
    {
        [Header("Prefabs & materials")] 
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private Material[] _teamMaterials;
        
        private ChessPiece[,] _chessPieces;
        private PiecePosition _piecePosition;
        private int _tileCountX;
        private int _tileCountY;

        public void Initialize(int tileCountX, int tileCountY,TileBoard tileBoard)
        {
            _tileCountX = tileCountX;
            _tileCountY = tileCountY;
            _piecePosition = new PiecePosition(tileCountX, tileCountY,tileBoard);
        }

        public ChessPiece[,] SpawnAllPieces()
        {
            _chessPieces = new ChessPiece[_tileCountX, _tileCountY];

            int whiteTeam = 0, blackTeam = 1;

            // White team

            _chessPieces[0, 0] = SpawnSinglePiece(ChessPieceType.Rook, whiteTeam);
            _chessPieces[1, 0] = SpawnSinglePiece(ChessPieceType.Knight, whiteTeam);
            _chessPieces[2, 0] = SpawnSinglePiece(ChessPieceType.Bishop, whiteTeam);
            _chessPieces[3, 0] = SpawnSinglePiece(ChessPieceType.King, whiteTeam);
            _chessPieces[4, 0] = SpawnSinglePiece(ChessPieceType.Queen, whiteTeam);
            _chessPieces[5, 0] = SpawnSinglePiece(ChessPieceType.Bishop, whiteTeam);
            _chessPieces[6, 0] = SpawnSinglePiece(ChessPieceType.Knight, whiteTeam);
            _chessPieces[7, 0] = SpawnSinglePiece(ChessPieceType.Rook, whiteTeam);

            // for (int i = 0; i < _tileCountX; i++) 
            //     _chessPieces[i, 1] = SpawnSinglePiece(ChessPieceType.Pawn, whiteTeam);

            //Black team

            _chessPieces[0, 7] = SpawnSinglePiece(ChessPieceType.Rook, blackTeam);
            _chessPieces[1, 7] = SpawnSinglePiece(ChessPieceType.Knight, blackTeam);
            _chessPieces[2, 7] = SpawnSinglePiece(ChessPieceType.Bishop, blackTeam);
            _chessPieces[3, 7] = SpawnSinglePiece(ChessPieceType.Queen, blackTeam);
            _chessPieces[4, 7] = SpawnSinglePiece(ChessPieceType.King, blackTeam);
            _chessPieces[5, 7] = SpawnSinglePiece(ChessPieceType.Bishop, blackTeam);
            _chessPieces[6, 7] = SpawnSinglePiece(ChessPieceType.Knight, blackTeam);
            _chessPieces[7, 7] = SpawnSinglePiece(ChessPieceType.Rook, blackTeam);

            // for (int i = 0; i < _tileCountX; i++) 
            //     _chessPieces[i, 6] = SpawnSinglePiece(ChessPieceType.Pawn, blackTeam);

            _piecePosition.PositionAllPieces(_chessPieces);
            
            return _chessPieces;
        }

        private ChessPiece SpawnSinglePiece(ChessPieceType type, int team)
        {
            ChessPiece cp = Instantiate(_prefabs[(int) type - 1], transform).GetComponent<ChessPiece>();
            cp.Initialize(type, team, _teamMaterials[team]);
            return cp;
        }

        public void GetPositionSinglePiece(int x, int y)
        {
            _piecePosition.PositionSinglePiece(x,y);
        }
    }
}