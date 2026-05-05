using System;
using UnityEngine;

namespace Bodybuilder.Map.Noise
{
    [Serializable]
    public class PerlinNoise : PerlinNoiseTexture
    {
        [SerializeField] private PerlinNoiseLayer _noise;
        
        protected override void GenerateTexture(Vector2 startPosition, ref Texture2D texture, Color[] pixels)
        {
            var position = new Vector2Int();
            var size = new Vector2(texture.width, texture.height);
            for (position.y = 0; position.y < texture.height; position.y++)
            {
                for (position.x = 0; position.x < texture.width; position.x++)
                {
                    var sample = _noise.SampleAt(startPosition + position / size, out var alpha);
                    pixels[position.y * texture.width + position.x] = new Color(sample, sample, sample, alpha);
                }
            }
            texture.SetPixels(pixels);
            texture.Apply();
        }
    }
}
