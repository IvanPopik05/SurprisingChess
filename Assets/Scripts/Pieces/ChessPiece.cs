using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Pieces
{
    public enum ChessPieceType
    {
        None = 0,
        Pawn = 1,
        Rook = 2,
        Bishop = 3,
        Knight = 4,
        Queen = 5,
        King = 6
    }

    public class ChessPiece : MonoBehaviour
    {
        [SerializeField] protected int _team;
        [SerializeField] protected ChessPieceType _chessPieceType;
        [SerializeField] protected int _currentX;
        [SerializeField] protected int _currentY;
        
        private Vector3 _desiredPosition;
        private Vector3 _desiredScale = Vector3.one;
        private MeshRenderer _meshRenderer;
        public int CurrentX => _currentX;
        public int CurrentY => _currentY;
        public int Team => _team;
        

        public ChessPieceType Type => _chessPieceType;
        public void Initialize(ChessPieceType type, int team, Material teamMaterial)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            
            transform.rotation = team == 1
                ? SetRotation(-90,0,-180)
                : SetRotation(-90,0,0);

            SetMaterial(teamMaterial);
            _chessPieceType = type;
            _team = team;
            
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position,_desiredPosition,Time.deltaTime * 10f);
            transform.localScale = Vector3.Lerp(transform.localScale,_desiredScale,Time.deltaTime * 10f);
        }

        public virtual List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int _tileCountX,int _tileCountY)
        {
            List<Vector2Int> r = new List<Vector2Int>();
            r.Add(new Vector2Int(3,3));
            r.Add(new Vector2Int(3,4));
            r.Add(new Vector2Int(4,3));
            r.Add(new Vector2Int(4,4));

            return r;
        }

        public virtual void SetPosition(Vector3 position, bool force = false)
        { 
            _desiredPosition = position;
            if (force) 
                transform.position = _desiredPosition;
        }

        public virtual void SetScale(Vector3 scale, bool force = false)
        {
            _desiredScale = scale;
            if (force) 
                transform.localScale = _desiredScale;
        }

        public void SetCurrentPosition(int x, int y)
        {
            _currentX = x;
            _currentY = y;
        }

        private void SetMaterial(Material teamMaterial)
        {
            Material[] newMaterials = _meshRenderer.materials;
            newMaterials[1] = teamMaterial;
            _meshRenderer.materials = newMaterials;
        }

        private Quaternion SetRotation(int x, int y, int z) => 
            Quaternion.Euler(new Vector3(x, y, z));
    }
}