using System.Collections.Generic;
using Bodybuilding;
using UnityEngine;

namespace Bodybuilder
{
    public class Layer : MonoBehaviour
    {
        [SerializeField] private Tile _tilePrefab;
        
        [SerializeField] private TileType _groundType;
        [SerializeField] private TileType _forestType;

        [SerializeField] private Vector2Int _offset;
        
        [SerializeField] private Vector2Int _size = new(20, 20);
        [SerializeField] private float _height = 1.0f;

        [SerializeField] private RGBPerlinNoise _noise;

        private Tile[,] _tiles;
        private Stack<Tile> _inactiveTiles = new();

        private void Start()
        {
            _noise.TextureSize = _size;
            _noise.Generate();
            
            _tiles = new Tile[_size.y, _size.x];
            var position = Vector2Int.zero;
            for (position.y = 0; position.y < _size.y; position.y++)
            {
                for (position.x = 0; position.x < _size.x; position.x++)
                {
                    var sample = _noise.Sample(position);
                    var alpha = _noise.SampleAlpha(position);
                    
                    var tile = PlaceTile(position, alpha.r > 0.5f ? alpha.g > 0.5f ? _forestType : _groundType : null);
                    if(tile) { tile.Elevation = sample.r * _height; }
                }
            }

            var offset = new Vector3(-_size.x / 2.0f + _offset.x, 0, _size.y + _offset.y);
            transform.localPosition = offset;
        }

        private Tile PlaceTile(Vector2Int position, TileType type)
        {
            if (type)
            {
                if (!_tiles[position.y, position.x])
                {
                    _tiles[position.y, position.x] =
                        _inactiveTiles.TryPop(out var tile)
                        ? tile
                        : Instantiate(_tilePrefab, transform);
                }
                _tiles[position.y, position.x].gameObject.SetActive(true);
                _tiles[position.y, position.x].gameObject.name = type.name;
                _tiles[position.y, position.x].Position = position;
                _tiles[position.y, position.x].Type = type;
            }
            else if (_tiles[position.y, position.x])
            {
                _inactiveTiles.Push(_tiles[position.y, position.x]);
                _tiles[position.y, position.x].gameObject.SetActive(false);
                _tiles[position.y, position.x] = null;
            }
            return _tiles[position.y, position.x];
        }
    }
}
