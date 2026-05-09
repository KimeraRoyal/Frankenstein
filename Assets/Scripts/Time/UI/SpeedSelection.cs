using System;
using UnityEngine;
using UnityEngine.Events;

namespace Bodybuilder.Time.UI
{
    public class SpeedSelection : MonoBehaviour
    {
        [Serializable]
        private struct SpeedSetting
        {
            [SerializeField] private float _speed;
            [SerializeField] private Sprite _icon;

            public float Speed => _speed;
            public Sprite Icon => _icon;
        }
        
        private Clock _clock;

        [SerializeField] private SpeedSetting[] _settings;
        [SerializeField] private int _currentSetting;
        private bool _blocked;

        public int CurrentSetting
        {
            get => _currentSetting;
            set
            {
                if(_blocked || _currentSetting == value) { return; }
                _currentSetting = value;
                _blocked = true;
                OnSelection?.Invoke(_currentSetting);
            }
        }

        public UnityEvent<int> OnSelection;

        private void Awake()
        {
            _clock = FindAnyObjectByType<Clock>();
            OnSelection.AddListener(index => _clock.Speed = _settings[index].Speed);
        }

        private void Start()
        { 
            OnSelection?.Invoke(_currentSetting);
        }

        private void LateUpdate()
        {
            _blocked = false;
        }

        public Sprite GetIcon(int index)
            => _settings[index].Icon;
    }
}