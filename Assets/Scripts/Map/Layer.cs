using Bodybuilding.Map.Tile;
using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilder.Map
{
    public class Layer : MonoBehaviour
    {
        [SerializeField] private Vector2Int _size;
        private float _elevation;
        private Vector3 _offset;
        private Vector3 _topLeft;

        private Tile[,] _tiles;

        public Vector2Int Size => _size;

        public float Elevation => _elevation;

        public Vector3 Offset => _offset;

        public Tile[,] Tiles => _tiles;

        public UnityEvent<Tile[,]> OnBuilt;

        public void Build(LayerInfo info, Vector2Int size)
        {
            _size = size;
            _elevation = info.Elevation;
            _offset = new Vector3(-_size.x, 0, _size.y) / 2.0f;
            
            _tiles = new Tile[_size.y, _size.x];
            var position = Vector2Int.zero;
            for (position.y = 0; position.y < _size.y; position.y++)
            {
                for (position.x = 0; position.x < _size.x; position.x++)
                {
                    _tiles[position.y, position.x] = new Tile(null, position);
                }
            }
            
            info.Build(_tiles);
            _topLeft = GetTilePosition(Vector2Int.zero);

            transform.localPosition = Vector3.up * _elevation;
            OnBuilt?.Invoke(_tiles);
        }

        public Tile GetTileAt(Vector3 position)
        {
            var relativePosition = position - _topLeft;
            
            // TODO: Clamps at edges, could be made to error instead
            var tile = new Vector2Int(Mathf.RoundToInt(relativePosition.x), Mathf.RoundToInt(-relativePosition.z));
            tile.x = Mathf.Clamp(tile.x, 0, _size.x - 1);
            tile.y = Mathf.Clamp(tile.y, 0, _size.y - 1);
            
            return _tiles[tile.y, tile.x];
        }
        
        public Vector3 GetTilePosition(Vector2Int tile)
        {
            tile.x = Mathf.Clamp(tile.x, 0, _size.x - 1);
            tile.y = Mathf.Clamp(tile.y, 0, _size.y - 1);
            return new Vector3(_tiles[tile.y, tile.x].Position.x, GetTileElevation(tile), -_tiles[tile.y, tile.x].Position.y) + Offset;
        }

        public float GetTileElevation(Vector2Int tile)
            => _tiles[tile.y, tile.x].Elevation + _elevation;
    }
}
