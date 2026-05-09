using Bodybuilder.Util.Time;
using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilder.Time
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] private TimePeriod _currentTime;
        [SerializeField] private Timer _timer;

        public UnityEvent<TimePeriod> OnIncrement;
        public UnityEvent<int> OnDay;
        public UnityEvent<int> OnHour;
        public UnityEvent<int> OnMinute;

        public UnityEvent<float> OnSpeedChanged;

        public float Speed
        {
            get => _timer.Speed;
            set
            {
                _timer.Speed = value;
                OnSpeedChanged?.Invoke(_timer.Speed);
            }
        }

        private void Awake()
        {
            _timer.OnInterval.AddListener(IncrementClock);
        }

        private void Start()
        {
            OnMinute?.Invoke(_currentTime.Minutes);
            OnHour?.Invoke(_currentTime.Hours);
            OnDay?.Invoke(_currentTime.Days);
            OnIncrement?.Invoke(_currentTime);
        }

        private void IncrementClock()
        {
            var currentHours = _currentTime.Hours;
            var currentDays = _currentTime.Days;
            _currentTime.Minutes++;
            OnMinute?.Invoke(_currentTime.Minutes);
            if(_currentTime.Hours != currentHours) { OnHour?.Invoke(_currentTime.Hours); }
            if(_currentTime.Days != currentDays) { OnDay?.Invoke(_currentTime.Days); }
            OnIncrement?.Invoke(_currentTime);
        }

        private void Update()
        {
            _timer.Update();
        }
    }
}
