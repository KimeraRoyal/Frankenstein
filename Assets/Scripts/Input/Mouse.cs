using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Bodybuilder.Input
{
    public class Mouse : MonoBehaviour
    {
        [Serializable]
        public class MouseButton
        {
            [SerializeField] private InputActionReference _buttonAction;
            private Vector2 _position;
            private bool _holding;

            public bool Held => _holding;

            public Action<Vector2> OnPressed;
            public Action<Vector2> OnReleased;
            
            public Action<Vector2, Vector2> OnDragged;

            public void RegisterActions()
            {
                _buttonAction.action.started += ButtonPressed;
                _buttonAction.action.canceled += ButtonReleased;
            }

            public void DeregisterActions()
            {
                _buttonAction.action.started -= ButtonPressed;
                _buttonAction.action.canceled -= ButtonReleased;
            }

            private void ButtonPressed(InputAction.CallbackContext obj)
            {
                _holding = true;
                OnPressed?.Invoke(_position);
            }

            private void ButtonReleased(InputAction.CallbackContext obj)
            {
                _holding = false;
                OnReleased?.Invoke(_position);
            }
            
            public void Drag(Vector2 position, Vector2 delta)
            {
                _position = position;
                if(!_holding) { return; }
                OnDragged?.Invoke(position, delta);
            }
        }
        
        [SerializeField] private Vector2Int _screenBorder;

        [SerializeField] private InputActionReference _mousePosition;
        [SerializeField] private MouseButton[] _buttons;

        private Vector2 _position;

        public MouseButton[] Buttons => _buttons;
        public MouseButton LMB => _buttons[0];
        public MouseButton RMB => _buttons[1];
        public MouseButton MMB => _buttons[2];
        
        public Vector2 Position => _position;
    
        public Action<Vector2, Vector2> OnMoved;

        private void Awake()
        {
            _mousePosition.action.performed += MouseMoved;
            foreach (var button in _buttons)
            {
                button.RegisterActions();
            }
        }

        private void OnDestroy()
        {
            _mousePosition.action.performed -= MouseMoved;
            foreach (var button in _buttons)
            {
                button.DeregisterActions();
            }
        }

        private void MouseMoved(InputAction.CallbackContext obj)
        {
            var position = _mousePosition.action.ReadValue<Vector2>();
            var delta = position - _position;
            if(Mathf.Abs(delta.magnitude) < 0.001f) { return; }
            _position = position;
            
            OnMoved?.Invoke(_position, delta);
            
            foreach (var button in _buttons)
            {
                button.Drag(_position, delta);
            }
        }
    }
}
