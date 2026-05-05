using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bodybuilder.Map.Noise
{
    [Serializable]
    public class PerlinNoiseLayer
    {
        private const float c_maxRandomOffset = 1000.0f;
        
        [SerializeField] private Vector2 _scale = Vector2.one;
        private Vector2 _offset;
        private bool _generatedOffset;
        
        [SerializeField] [Range(0.0f, 1.0f)] private float _cutoffMin = 0.0f, _cutoffMax = 1.0f;
        [SerializeField] private bool _invertCutoff;

        public float SampleAt(Vector2 samplePosition, out float alpha)
        {
            if (!_generatedOffset)
            {
                _offset = new Vector2(Random.Range(0.0f, c_maxRandomOffset), Random.Range(0.0f, c_maxRandomOffset));
            }
            
            var sample = samplePosition * _scale;
            var noise = Mathf.PerlinNoise(sample.x, sample.y);
            alpha = (_cutoffMin < 0.001f || noise > _cutoffMin) && (_cutoffMax > 0.999f || noise < _cutoffMax) ? 1.0f : 0.0f;
            if (_invertCutoff) { alpha = 1.0f - alpha; }
            return noise;
        }
    }
}