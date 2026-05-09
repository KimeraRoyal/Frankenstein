using Bodybuilding.Map.Tile;
using UnityEngine;

namespace Bodybuilder.Map
{
    [RequireComponent(typeof(Layer), typeof(MeshFilter), typeof(MeshCollider))]
    public class LayerRenderer : MonoBehaviour
    {
        private const float MinPosition = -0.5f, MaxPosition = 0.5f;
        
        private static readonly Vector3[] TileVertices =
        {
            new (MinPosition, 0.0f, MinPosition),
            new (MaxPosition, 0.0f, MinPosition),
            new (MinPosition, 0.0f, MaxPosition),
            new (MaxPosition, 0.0f, MaxPosition)
        };

        private static readonly Vector2[] TileUV =
        {
            new(0.0f, 0.0f),
            new(1.0f, 0.0f),
            new(0.0f, 1.0f),
            new(1.0f, 1.0f)
        };

        private static readonly int[] TileIndices =
        {
            0, 2, 1,
            2, 3, 1
        };
        
        [SerializeField] private Layer _layer;
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshCollider _collider;

        private void Awake()
        {
            _layer.OnBuilt.AddListener(CreateMesh);
        }

        public void CreateMesh(Tile[,] tiles)
        {
            var mesh = new Mesh();

            var faceCount = tiles.GetLength(0) * tiles.GetLength(1);
            var vertexCount = faceCount * 4;
            
            var vertices = new Vector3[vertexCount];
            var normals = new Vector3[vertexCount];
            var uv = new Vector2[vertexCount];
            var colors = new Color[vertexCount];
            var tris = new int[faceCount * 6];

            var tileIndex = Vector2Int.zero;
            var tilePosition = Vector3.zero;
            for (tileIndex.y = 0; tileIndex.y < tiles.GetLength(0); tileIndex.y++)
            {
                for (tileIndex.x = 0; tileIndex.x < tiles.GetLength(1); tileIndex.x++)
                {
                    var index = tileIndex.x + tileIndex.y * tiles.GetLength(1);

                    tilePosition.x = tiles[tileIndex.y, tileIndex.x].Position.x;
                    tilePosition.y = tiles[tileIndex.y, tileIndex.x].Elevation;
                    tilePosition.z = -tiles[tileIndex.y, tileIndex.x].Position.y;
                    for (var i = 0; i < TileVertices.Length; i++)
                    {
                        vertices[index * 4 + i] = TileVertices[i] + tilePosition + _layer.Offset;
                        normals[index * 4 + i] = Vector3.up;
                        uv[index * 4 + i] = TileUV[i];
                        colors[index * 4 + i] = tiles[tileIndex.y, tileIndex.x].Type ? tiles[tileIndex.y, tileIndex.x].Type.Color : Color.white;
                    }
                    for (var i = 0; i < TileIndices.Length; i++)
                    {
                        tris[index * 6 + i] = TileIndices[i] + index * 4;
                    }
                }
            }
            
            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uv;
            mesh.colors = colors;
            mesh.triangles = tris;

            _meshFilter.mesh = mesh;
            _collider.sharedMesh = mesh;
        }

        private void OnValidate()
        {
            _layer = GetComponent<Layer>();
            _meshFilter = GetComponent<MeshFilter>();
            _collider = GetComponent<MeshCollider>();
        }
    }
}