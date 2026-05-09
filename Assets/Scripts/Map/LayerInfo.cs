using System;
using Bodybuilder.Map.Builder;
using Bodybuilding.Map.Tile;
using UnityEngine;

namespace Bodybuilder.Map
{
    [Serializable]
    public struct LayerInfo
    {
        [SerializeField] private float _elevation;
        
        [SerializeField] private LayerBuilder[] _builders;

        public float Elevation => _elevation;

        public void Build(Tile[,] tiles)
        {
            foreach (var builder in _builders)
            {
                builder.BuildLayer(tiles);
            }
        }
    }
}