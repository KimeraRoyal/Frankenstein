using Bodybuilder.Util.Noise;
using Bodybuilding.Map.Tiles;
using UnityEngine;

namespace Bodybuilder.Map.Layers.Builder
{
    [CreateAssetMenu(fileName = "Ground Layer Builder", menuName = "Bodybuilding/Ground Layer Builder")]
    public class GroundLayerBuilder : LayerBuilder
    {
        [SerializeField] private TileType _groundTile;
        [SerializeField] private TileType _forestTile;
        [SerializeField] private TileType _waterTile;
        
        [SerializeField] private float _height = 1.0f;
        [SerializeField] [Range(0.0f, 1.0f)] private float _minSample = 0.0f, _maxSample = 1.0f;

        [SerializeField] private RGBPerlinNoise _noise;
        
        public override void BuildLayer(Tile[,] tiles)
        {
            var size = new Vector2Int(tiles.GetLength(0), tiles.GetLength(1));
            _noise.TextureSize = size;
            _noise.Generate();
            
            var position = Vector2Int.zero;
            for (position.y = 0; position.y < size.y; position.y++)
            {
                for (position.x = 0; position.x < size.x; position.x++)
                {
                    var sample = _noise.Sample(position);
                    var alpha = _noise.SampleAlpha(position);
                    
                    tiles[position.y, position.x].Type = alpha.r > 0.5f ? alpha.g > 0.5f ? _forestTile : _groundTile : _waterTile;
                    tiles[position.y, position.x].Elevation = (Mathf.Clamp(sample.r, _minSample, _maxSample) - _minSample) / (_maxSample - _minSample) * _height;
                }
            }
            
            _noise.Clean();
        }
    }
}