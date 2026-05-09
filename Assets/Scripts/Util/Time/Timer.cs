using System;
using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilder.Util.Time
{
    [Serializable]
    public class Timer
    {
        [SerializeField] private bool _running;
        [SerializeField] [Min(0.0f)] private float _speed = 1.0f;
        [SerializeField] private float _interval = 1.0f;
        [SerializeField] private bool _loop;
        private float _timer;

        public bool Running => _running;

        public float Speed { get => _speed; set => _speed = Mathf.Max(0.0f, value); }
        public float Interval { get => _interval; set => _interval = value; }

        public float PercentageCompletion => Mathf.Clamp01(_timer / _interval);

        public UnityEvent OnInterval;

        public Timer(float interval = 1.0f, bool loop = true, bool startInstantly = false)
        {
            _interval = interval;
            _loop = loop;
            _running = startInstantly;
        }
        
        public void Start()
        {
            _running = true;
        }

        public void Pause()
        {
            _running = false;
        }

        public void Stop()
        {
            _timer = 0.0f;
            _running = false;
        }

        public void Update()
        {
            if(!_running) { return; }
            _timer += UnityEngine.Time.deltaTime * _speed;
            if(_timer < _interval) { return; }
            _timer -= _interval;
            if(!_loop) { Stop(); }
            OnInterval?.Invoke();
        }
    }
}