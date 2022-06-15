using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Pieces;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    private const int TILE_COUNT_X = 8;
    private const int TILE_COUNT_Y = 8;

    [Header("Art board")]
    [SerializeField] private Material _hoverMaterial;
    [SerializeField] private Material _tileMaterial;

    [Header("Prefabs & materials")] 
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private Material[] _teamMaterials;
    
    [Header("UI")]
    [SerializeField] private WinDisplay _winDisplay;

    [Header("Objects")] 
    [SerializeField] private CameraReversal _cameraReversal;
    [Header("Parameters")]
    [SerializeField]private float _deathSize = 0.5f;
    [SerializeField]private float _deathSpacing = 0.3f;
    [SerializeField]private float _dragOffset = 0.4f;
    [SerializeField] private float _tileSize = 1;
    [SerializeField] private float _yOffset = 0.5f;
    [SerializeField] private Vector3 _boardCenter = Vector3.zero;

    private List<ChessPiece> deadBlackPieces = new List<ChessPiece>();
    private List<ChessPiece> deadWhitePieces = new List<ChessPiece>();
    
    private ChessPiece[,] _chessPieces;
    private ChessPiece _currentlyDragging;
    private List<Vector2Int> _availableMoves;
    private Camera _currentCamera;
    private Vector2Int _currentHover;

    private bool isWhiteTurn;

    private GenerateTile _generateTile;
    private void Awake() => 
        Initialize();

    private void Initialize()
    {
        _currentCamera = Camera.main;
        _winDisplay.Initialize(this);

        _generateTile = new GenerateTile(_tileMaterial,_yOffset);
        _generateTile.GenerateTiles(_tileSize,TILE_COUNT_X,TILE_COUNT_Y,transform);

        isWhiteTurn = true;
        _cameraReversal.ChangeLocationCamera(isWhiteTurn);
        
        SpawnAllPieces();
        PositionAllPieces();
    }

    private void Update()
    {
        if (!_currentCamera)
            return;

        Ray ray = _currentCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, 100,LayerMask.GetMask("Tile","Hover","HighLight")))
        {
            Vector2Int hitPosition = LookupTileIndex(hit.transform.gameObject);
            
            if (_currentHover == -Vector2Int.one)
            {
                _currentHover = hitPosition;
                _generateTile.Tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
                _generateTile.MeshRenderers[hitPosition.x, hitPosition.y].material = _hoverMaterial;
            }

            if (_currentHover != hitPosition)
            {
                _generateTile.Tiles[_currentHover.x, _currentHover.y].layer = ContainsValidMove(ref _availableMoves,_currentHover) 
                    ? LayerMask.NameToLayer("HighLight") 
                    : LayerMask.NameToLayer("Tile");;
                _generateTile.MeshRenderers[_currentHover.x, _currentHover.y].material = _tileMaterial;
                _currentHover = hitPosition;
                _generateTile.Tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
                _generateTile.MeshRenderers[hitPosition.x, hitPosition.y].material = _hoverMaterial;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (_chessPieces[hitPosition.x, hitPosition.y])
                {
                    if ((_chessPieces[hitPosition.x, hitPosition.y].Team == 0 && isWhiteTurn) 
                        || (_chessPieces[hitPosition.x, hitPosition.y].Team == 1 && !isWhiteTurn))
                    {
                        _currentlyDragging = _chessPieces[hitPosition.x, hitPosition.y];

                        _availableMoves = _currentlyDragging.GetAvailableMoves(ref _chessPieces, TILE_COUNT_X, TILE_COUNT_Y);

                        HighLightTiles();
                    }
                }
            }

            if (_currentlyDragging && Input.GetMouseButtonUp(0))
            {
                Vector2Int previousPosition = new Vector2Int(_currentlyDragging.CurrentX, _currentlyDragging.CurrentY);

                bool isMove = MoveTo(_currentlyDragging, hitPosition.x, hitPosition.y);

                if (!isMove)
                    _currentlyDragging.SetPosition(GetTileCenter(previousPosition.x, previousPosition.y));

                _currentlyDragging = null;
                RemoveHighLightTiles();
            }
        }
        else
        {
            if (_currentHover != -Vector2Int.one)
            {
                _generateTile.Tiles[_currentHover.x, _currentHover.y].layer = ContainsValidMove(ref _availableMoves,_currentHover) 
                    ? LayerMask.NameToLayer("HighLight") 
                    : LayerMask.NameToLayer("Tile");
                _generateTile.MeshRenderers[_currentHover.x, _currentHover.y].material = _tileMaterial;

                _currentHover = -Vector2Int.one;
            }

            if (_currentlyDragging && Input.GetMouseButtonUp(0))
            {
                _currentlyDragging.SetPosition(GetTileCenter(_currentlyDragging.CurrentX,_currentlyDragging.CurrentY));
                _currentlyDragging = null;
                RemoveHighLightTiles();
            }
        }

        if (_currentlyDragging)
        {
            Plane plane = new Plane(Vector3.up, Vector3.up * _generateTile.YOffset);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 point = ray.GetPoint(distance);
                _currentlyDragging.SetPosition(point + Vector3.up * _dragOffset);
            }
        }
    }



    // Generate tiles & meshRenders

    // private void GenerateTiles(float tileSize, int tileCountX, int tileCountY)
    // {
    //     _yOffset += transform.position.y;
    //     _bounds = new Vector3((tileCountX / 2) * _tileSize,0,(tileCountY / 2) * tileSize);
    //     
    //     _tiles = new GameObject[tileCountX,tileCountY];
    //     _meshRenderers = new MeshRenderer[tileCountX,tileCountY];
    //     for (int x = 0; x < tileCountX; x++)
    //     {
    //         for (int y = 0; y < tileCountY; y++)
    //         {
    //             _tiles[x, y] = GenerateSingleTile(tileSize, x, y);
    //             _meshRenderers[x,y] = GenerateMeshRenderers(_tiles[x,y]);
    //         }
    //     }
    // }
    //
    // private GameObject GenerateSingleTile(float tileSize, int x, int y)
    // {
    //     GameObject tile = new GameObject($"X:{x},Y:{y}");
    //     tile.transform.SetParent(transform);
    //
    //     Mesh mesh = new Mesh();
    //     tile.AddComponent<MeshFilter>().mesh = mesh;
    //     tile.AddComponent<MeshRenderer>().material = _tileMaterial;
    //     Vector3[] vertices = new Vector3[4];
    //     vertices[0] = new Vector3(x * tileSize,_yOffset,y * tileSize) - _bounds;
    //     vertices[1] = new Vector3(x * tileSize,_yOffset,(y+1) * tileSize) - _bounds;
    //     vertices[2] = new Vector3((x+1) * tileSize,_yOffset,y * tileSize) - _bounds;
    //     vertices[3] = new Vector3((x+1) * tileSize,_yOffset,(y+1) * tileSize) - _bounds;
    //
    //     int[] tris = {0,1,2,1,3,2};
    //     mesh.vertices = vertices;
    //     mesh.triangles = tris;
    //
    //     mesh.RecalculateNormals();
    //     tile.layer = LayerMask.NameToLayer("Tile");
    //     tile.AddComponent<BoxCollider>();
    //     return tile;
    // }
    //
    // private MeshRenderer GenerateMeshRenderers(GameObject tile) => 
    //     tile.GetComponent<MeshRenderer>();


    // Spawning pieces

    private void SpawnAllPieces()
    {
        _chessPieces = new ChessPiece[TILE_COUNT_X,TILE_COUNT_Y];
        
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

        // for (int i = 0; i < TILE_COUNT_X; i++) 
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

        // for (int i = 0; i < TILE_COUNT_X; i++) 
        //     _chessPieces[i, 6] = SpawnSinglePiece(ChessPieceType.Pawn, blackTeam);
    }

    private ChessPiece SpawnSinglePiece(ChessPieceType type, int team)
    {
        ChessPiece cp = Instantiate(_prefabs[(int) type - 1], transform).GetComponent<ChessPiece>();
        cp.Initialize(type,team,_teamMaterials[team]);
        return cp;
    }


    // Position

    private void PositionAllPieces()
    {
        for (int x = 0; x < TILE_COUNT_X; x++)
            for (int y = 0; y < TILE_COUNT_Y; y++)
                if (_chessPieces[x, y])
                    PositionSinglePiece(x, y, true);
    }

    private void PositionSinglePiece(int x, int y, bool force = false)
    {
        _chessPieces[x,y].SetCurrentPosition(x,y);
        _chessPieces[x,y].SetPosition(GetTileCenter(x,y),force);
    }


    private void Checkmate(int team)
    {
        _winDisplay.ShowWinTeam(team);
    }

    // Operations

    public void ResetChessBoard()
    {
        for (int x = 0; x < TILE_COUNT_X; x++)
        {
            for (int y = 0; y < TILE_COUNT_Y; y++)
            {
                if (_chessPieces[x, y])
                {
                    Destroy(_chessPieces[x, y].gameObject);
                    _chessPieces[x, y] = null;   
                }
            }
        }

        for (int i = 0; i < deadWhitePieces.Count; i++) 
            Destroy(deadWhitePieces[i].gameObject);
        
        for (int i = 0; i < deadBlackPieces.Count; i++) 
            Destroy(deadBlackPieces[i].gameObject);
        
        deadWhitePieces.Clear();
        deadBlackPieces.Clear();

        SpawnAllPieces();
        PositionAllPieces();
        
        isWhiteTurn = true;
        _cameraReversal.ChangeLocationCamera(isWhiteTurn);
    }

    private bool MoveTo(ChessPiece cp, int x, int y)
    {
        if (!ContainsValidMove(ref _availableMoves, new Vector2Int(x, y)))
            return false;
        
        Vector2Int previousPosition = new Vector2Int(cp.CurrentX,cp.CurrentY);

        if (_chessPieces[x, y])
        {
            ChessPiece chessPiece = _chessPieces[x, y];
            if (chessPiece.Team == cp.Team)
            {
                return false;
            }

            if (chessPiece.Team == 0 )
            {
                deadWhitePieces.Add(chessPiece);
                chessPiece.SetScale(Vector3.one * _deathSize);
                chessPiece.SetPosition(new Vector3(8 * _tileSize, _generateTile.YOffset, -1 * _tileSize) - _generateTile.Bounds + 
                                       new Vector3(_tileSize / 2, 0, _tileSize / 2) + 
                                       Vector3.forward * (_deathSpacing * deadWhitePieces.Count));

                if (chessPiece.Type == ChessPieceType.King)
                {
                    Checkmate(1);
                }
            }
            else
            {
                deadBlackPieces.Add(chessPiece);
                chessPiece.SetScale(Vector3.one * _deathSize);
                chessPiece.SetPosition(new Vector3(-1 * _tileSize, _generateTile.YOffset, 8 * _tileSize) - _generateTile.Bounds + 
                                       new Vector3(_tileSize / 2, 0, _tileSize / 2) + 
                                       Vector3.back * (_deathSpacing * deadBlackPieces.Count));
                
                if (chessPiece.Type == ChessPieceType.King)
                {
                    Checkmate(0);
                }
            }
        }

        _chessPieces[x, y] = cp;
        _chessPieces[previousPosition.x, previousPosition.y] = null;
        
        PositionSinglePiece(x,y);

        isWhiteTurn = !isWhiteTurn;
        _cameraReversal.ChangeLocationCamera(isWhiteTurn);
        return true;
    }

    private void HighLightTiles()
    {
        for (int i = 0; i < _availableMoves.Count; i++)
            _generateTile.Tiles[_availableMoves[i].x, _availableMoves[i].y].layer = LayerMask.NameToLayer("HighLight");
    }

    private void RemoveHighLightTiles()
    {
        for (int i = 0; i < _availableMoves.Count; i++)
            _generateTile.Tiles[_availableMoves[i].x, _availableMoves[i].y].layer = LayerMask.NameToLayer("Tile");

        _availableMoves.Clear();
    }

    private bool ContainsValidMove(ref List<Vector2Int> moves, Vector2Int pos)
    {
        if (moves == null)
            return false;
        
        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i].x == pos.x && moves[i].y == pos.y)
                return true;
        }

        return false;
    }

    private Vector2Int LookupTileIndex(GameObject tile)
    {
        for (int x = 0; x < TILE_COUNT_X; x++)
        for (int y = 0; y < TILE_COUNT_Y; y++)
            if ( _generateTile.Tiles[x, y] == tile)
                return new Vector2Int(x, y);

        return -Vector2Int.one;
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        return new Vector3(x * _tileSize, _generateTile.YOffset, y * _tileSize) - _generateTile.Bounds + new Vector3(_tileSize/2,0,_tileSize/2);
    }
}