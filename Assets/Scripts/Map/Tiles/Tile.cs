using System;
using System.Linq;
using UnityEngine;

namespace Bodybuilding.Map.Tiles
{
    public class Tile
    {
        private TileType _type;

        private int _layerIndex;
        private Vector2Int _position;
        private float _elevation;

        private TileFeature[] _features;
        private int _featureCount;

        public TileType Type
        {
            get => _type;
            set
            {
                _type = value;
                _features = new TileFeature[_type.MaxFeatures];
            }
        }

        public int LayerIndex { get => _layerIndex; set => _layerIndex = value; }
        public Vector2Int Position { get => _position; set => _position = value; }
        public float Elevation { get => _elevation; set => _elevation = value; }

        public Func<Tile, Direction, Tile> FindAdjacentTile;

        public TileFeature[] CurrentFeatures => _features[.._featureCount];
        public int CurrentFeatureCount => _featureCount;
        public int MaxFeatureCount => _features.Length;
        public bool CanFitFeature => _featureCount < _features.Length;

        public Tile(TileType type, int layerIndex, Vector2Int position, float elevation = 0.0f)
        {
            _type = type;

            _layerIndex = layerIndex;
            _position = position;
            _elevation = elevation;
        }

        public Tile GetAdjacentTile(Direction direction)
            => FindAdjacentTile.Invoke(this, direction);
        
        /// <summary>
        /// FOR USE BY TileFeature ONLY
        /// USE TileFeature.MoveToTile(tile) INSTEAD!!!
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public bool AddFeature(TileFeature feature)
        {
            if (!CanFitFeature || _features.Contains(feature)) { return false; }
            _features[_featureCount++] = feature;
            return true;
        }

        public void RemoveFeature(TileFeature feature)
        {
            if (MaxFeatureCount == 1 && _features[0] == feature)
            {
                _features[0] = null;
                _featureCount = 0;
                return;
            }
            
            var found = false;
            for (var i = 0; i < _featureCount; i++)
            {
                if(!found)
                {
                    found = _features[i] == feature;
                    continue;
                }
                _features[i - 1] = _features[i];
            }

            if (found) { _featureCount--; }
        }
    }
}
