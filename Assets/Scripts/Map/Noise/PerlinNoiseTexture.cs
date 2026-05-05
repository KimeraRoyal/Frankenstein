using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Bodybuilder.Map.Noise
{
    [Serializable]
    public abstract class PerlinNoiseTexture
    {
        private const float c_maxRandomSamplePosition = 1000.0f;
        
        [SerializeField] private Vector2Int _textureSize = Vector2Int.one * 32;
        [SerializeField] private Texture2D _noiseTexture;
        private Color[] _pixels;

        public Vector2Int TextureSize
        {
            get => _textureSize;
            set
            {
                Clean();
                _textureSize = value;
            }
        }
        public Texture2D NoiseTexture => _noiseTexture;

        public void Generate(Vector2 startPosition)
        {
            Clean();
            _noiseTexture = new Texture2D(_textureSize.x, _textureSize.y);
            _pixels = new Color[_textureSize.x * _textureSize.y];
            GenerateTexture(startPosition, ref _noiseTexture, _pixels);
        }

        public void Generate()
            => Generate(new Vector2(Random.Range(0.0f, c_maxRandomSamplePosition), Random.Range(0.0f, c_maxRandomSamplePosition)));

        public void Regenerate(Vector2 startPosition)
            => GenerateTexture(startPosition, ref _noiseTexture, _pixels);

        public void Regenerate()
            => Regenerate(new Vector2(Random.Range(0.0f, c_maxRandomSamplePosition), Random.Range(0.0f, c_maxRandomSamplePosition)));
        
        public void Clean()
        {
            if(!_noiseTexture) { return; }
            Object.Destroy(_noiseTexture);
            _pixels = null;
        }

        public Color Sample(Vector2Int pixel)
            => _pixels[pixel.y * _textureSize.x + pixel.x];

        protected abstract void GenerateTexture(Vector2 startPosition, ref Texture2D texture, Color[] pixels);
    }
}