﻿using UnityEngine;

namespace DefaultNamespace
{
    public class GenerateTile
    {
        
        private Material _tileMaterial;
        private GameObject[,] _tiles;
        private MeshRenderer[,] _meshRenderers;

        private float _yOffset;
        private Vector3 _bounds;

        public Vector3 Bounds => _bounds;
        public MeshRenderer[,] MeshRenderers => _meshRenderers;
        public GameObject[,] Tiles => _tiles;

        public float YOffset => _yOffset;
        
        public GenerateTile(Material tileMaterial, float yOffset)
        {
            _tileMaterial = tileMaterial;
            _yOffset = yOffset;
        }
        
        
        public void GenerateTiles(float tileSize, int tileCountX, int tileCountY, Transform parent)
        {
            _yOffset += parent.position.y;
            _bounds = new Vector3((tileCountX / 2) * tileSize,0,(tileCountY / 2) * tileSize);
        
            _tiles = new GameObject[tileCountX,tileCountY];
            _meshRenderers = new MeshRenderer[tileCountX,tileCountY];
            for (int x = 0; x < tileCountX; x++)
            {
                for (int y = 0; y < tileCountY; y++)
                {
                    _tiles[x, y] = GenerateSingleTile(tileSize, x, y,parent);
                    _meshRenderers[x,y] = GenerateMeshRenderers(_tiles[x,y]);
                }
            }
        }

        private GameObject GenerateSingleTile(float tileSize, int x, int y,Transform parent)
        {
            GameObject tile = new GameObject($"X:{x},Y:{y}");
            tile.transform.SetParent(parent);

            Mesh mesh = new Mesh();
            tile.AddComponent<MeshFilter>().mesh = mesh;
            tile.AddComponent<MeshRenderer>().material = _tileMaterial;
            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(x * tileSize,_yOffset,y * tileSize) - _bounds;
            vertices[1] = new Vector3(x * tileSize,_yOffset,(y+1) * tileSize) - _bounds;
            vertices[2] = new Vector3((x+1) * tileSize,_yOffset,y * tileSize) - _bounds;
            vertices[3] = new Vector3((x+1) * tileSize,_yOffset,(y+1) * tileSize) - _bounds;

            int[] tris = {0,1,2,1,3,2};
            mesh.vertices = vertices;
            mesh.triangles = tris;

            mesh.RecalculateNormals();
            tile.layer = LayerMask.NameToLayer("Tile");
            tile.AddComponent<BoxCollider>();
            return tile;
        }

        private MeshRenderer GenerateMeshRenderers(GameObject tile) => 
            tile.GetComponent<MeshRenderer>();
    }
}