using System;
using Bodybuilding.Map.Tiles;
using UnityEngine;

namespace Bodybuilder.Map.Layers
{
    [Serializable]
    public class LayerInfo
    {
        [SerializeField] private float _elevation;

        [SerializeField] private bool _selectable = true;
        
        [SerializeField] private LayerBuilder[] _builders;

        public float Elevation => _elevation;

        public bool Selectable => _selectable;

        public void Build(Tile[,] tiles)
        {
            foreach (var builder in _builders)
            {
                builder.BuildLayer(tiles);
            }
        }
    }
}