using System.Collections.Generic;
using Bodybuilder.Map.Builder;
using Bodybuilder.Map.Noise;
using Bodybuilding.Map.Tile;
using UnityEngine;

namespace Bodybuilder.Map
{
    public class Layer : MonoBehaviour
    {
        [SerializeField] private Tile _tilePrefab;
        
        [SerializeField] private Vector3Int _offset;
        [SerializeField] private Vector2Int _size = new(20, 20);

        [SerializeField] private LayerBuilder[] _builders;

        private Tile[,] _tiles;
        private readonly Stack<Tile> _inactiveTiles = new();

        private void Start()
        {
            var types = new TileType[_size.y, _size.x];
            foreach (var builder in _builders)
            {
                builder.BuildLayer(types);
            }
            
            _tiles = new Tile[_size.y, _size.x];
            var position = Vector2Int.zero;
            for (position.y = 0; position.y < _size.y; position.y++)
            {
                for (position.x = 0; position.x < _size.x; position.x++)
                {
                    _tiles[position.y, position.x] = PlaceTile(position, types[position.y, position.x]);
                }
            }

            var offset = new Vector3(-_size.x / 2.0f + _offset.x, _offset.y, _size.y + _offset.z);
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
