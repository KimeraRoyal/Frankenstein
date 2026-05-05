using System;
using UnityEngine;

namespace Bodybuilder.Map.Noise
{
    [Serializable]
    public class RGBPerlinNoise : PerlinNoiseTexture
    {
        private enum ColorAlphaBehaviour
        {
            TakeFromRed, 
            TakeFromGreen, 
            TakeFromBlue, 
            CombineAll
        }
        
        [SerializeField] private PerlinNoiseLayer _r;
        [SerializeField] private PerlinNoiseLayer _g;
        [SerializeField] private PerlinNoiseLayer _b;

        private Vector2 _gOffset, _bOffset;
        private bool _generatedOffsets;

        [SerializeField] private ColorAlphaBehaviour _alphaBehaviour;
        private Color[] _alphaValues;

        protected override void GenerateTexture(Vector2 startPosition, ref Texture2D texture, Color[] pixels)
        {
            if (_alphaValues == null || _alphaValues.Length != pixels.Length)
            {
                _alphaValues = new Color[pixels.Length];
            }
            var position = new Vector2Int();
            var size = new Vector2(texture.width, texture.height);
            var color = new Color();
            for (position.y = 0; position.y < texture.height; position.y++)
            {
                for (position.x = 0; position.x < texture.width; position.x++)
                {
                    var i = position.y * texture.width + position.x;
                    color.r = _r.SampleAt(startPosition + position / size, out _alphaValues[i].r);
                    color.g = _g.SampleAt(startPosition + position / size, out _alphaValues[i].g);
                    color.b = _b.SampleAt(startPosition + position / size, out _alphaValues[i].b);
                    _alphaValues[i].a = _alphaValues[i].r * _alphaValues[i].g * _alphaValues[i].b;
                    color.a = _alphaValues[i][(int) _alphaBehaviour];
                    pixels[i] = color;
                }
            }
            texture.SetPixels(pixels);
            texture.Apply();
        }

        public Color SampleAlpha(Vector2Int pixel)
            => _alphaValues[pixel.y * TextureSize.x + pixel.x];
    }
}