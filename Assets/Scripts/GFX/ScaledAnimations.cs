using System;
using Bodybuilder.Util.Time;
using UnityEngine;

namespace Bodybuilder.GFX
{
    public class ScaledAnimations : MonoBehaviour
    {
        [Serializable]
        private class FrameAnimation
        {
            [SerializeField] private float _minStrength;
            [SerializeField] private Sprite[] _sprites;

            public float MinStrength => _minStrength;

            public Sprite GetSpriteAtFrame(ref int frame)
            {
                if (_sprites.Length < 1)
                {
                    frame = 0;
                    return null;
                }
                frame %= _sprites.Length;
                return _sprites[frame];
            }
        }

        private SpriteRenderer _renderer;

        [SerializeField] private FrameAnimation[] _animations;
        [SerializeField] private Timer _frameTimer = new(0.1f);
        private int _currentAnimation;
        private int _currentFrame;

        [SerializeField] [Range(0.0f, 1.0f)] private float _strength;

        public float Strength
        {
            get => _strength;
            set
            {
                _strength = Mathf.Clamp01(value);
                DetermineAnimation();
            }
        }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _frameTimer.OnInterval.AddListener(NextFrame);
        }

        private void Start()
        {
            DetermineAnimation();
            _frameTimer.Start();
        }

        private void Update()
        {
            _frameTimer.Update();
        }

        private void DetermineAnimation()
        {
            _currentAnimation = 0;
            for (var i = 1; i < _animations.Length; i++)
            {
                if (_animations[i].MinStrength > _strength) { break; }
                _currentAnimation = i;
            }
            _renderer.sprite = _animations[_currentAnimation].GetSpriteAtFrame(ref _currentFrame);
        }

        private void NextFrame()
        {
            _currentFrame++;
            _renderer.sprite = _animations[_currentAnimation].GetSpriteAtFrame(ref _currentFrame);
        }

        private void OnValidate()
        {
            if (_animations == null || _animations.Length < 1)
            {
                _animations = new FrameAnimation[1];
                _animations[0] = new FrameAnimation();
            }
        }
    }
}
