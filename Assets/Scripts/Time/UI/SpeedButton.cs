using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Bodybuilder.Time.UI
{
    [RequireComponent(typeof(Button))]
    public class SpeedButton : MonoBehaviour
    {
        private Button _button;
        private SpeedSelection _selection;

        [SerializeField] private bool _interactable;

        [SerializeField] private Image _icon;
        [SerializeField] private Image _bg;

        [SerializeField] private Color _unselectedColor = Color.white;
        [SerializeField] private Color _selectedColor = Color.white;

        [SerializeField] private InputActionReference _key;
        
        [SerializeField] private int[] _settingIndices;
        private int _currentIndex;

        public bool MultipleOptions => _settingIndices.Length > 1;

        private void Awake()
        {
            _button.onClick.AddListener(Click);
            _selection.OnSelection.AddListener(SelectionChanged);
            
            _key.action.started += KeyPressed;
        }

        private void OnDestroy()
        {
            _key.action.started -= KeyPressed;
        }

        private void Click()
        {
            if(!_interactable) { return; }
            _selection.CurrentSetting = _settingIndices[_currentIndex];
        }

        private void SelectionChanged(int selection)
        {
            var selected = _settingIndices.Contains(selection);
            _interactable = MultipleOptions || !selected;
            
            _currentIndex = 0;
            if (selected && MultipleOptions)
            {
                _currentIndex = Array.IndexOf(_settingIndices, selection);
            }
            
            _icon.sprite = _selection.GetIcon(_settingIndices[_currentIndex]);
            _bg.color = selected ? _selectedColor : _unselectedColor;

            if (selected && MultipleOptions)
            {
                _currentIndex = (_currentIndex + 1) % _settingIndices.Length;
            }
        }

        private void KeyPressed(InputAction.CallbackContext obj)
        {
            Click();
        }

        private void OnValidate()
        {
            _button = GetComponent<Button>();
            _selection = GetComponentInParent<SpeedSelection>();
            
            if (_settingIndices == null || _settingIndices.Length < 1)
            {
                _settingIndices = new int[1];
            }
        }
    }
}
