using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Bodybuilder.Input
{
    public class Mouse : MonoBehaviour
    {
        [SerializeField] private Vector2Int _screenBorder;

        [SerializeField] private InputActionReference _mousePosition;
        [SerializeField] private InputActionReference _mouseLeftButton;

        private Vector2 _position;
        private Vector2 _previousPosition;
        private bool _holding;

        public Action<Vector2> OnPressed;
        public Action<Vector2> OnReleased;
    
        public Action<Vector2, Vector2> OnMoved;
        public Action<Vector2, Vector2> OnDragged;

        private void Awake()
        {
            _mousePosition.action.performed += MouseMoved;
            _mouseLeftButton.action.started += LmbPressed;
            _mouseLeftButton.action.canceled += LmbReleased;
        }

        private void MouseMoved(InputAction.CallbackContext obj)
        {
            _position = _mousePosition.action.ReadValue<Vector2>();
            Move();
            _previousPosition = _position;
        }

        private void LmbPressed(InputAction.CallbackContext obj)
        {
            _holding = true;
            _position = _mousePosition.action.ReadValue<Vector2>();
            OnPressed?.Invoke(_position);
        }

        private void LmbReleased(InputAction.CallbackContext obj)
        {
            _holding = false;
            _position = _mousePosition.action.ReadValue<Vector2>();
            OnReleased?.Invoke(_position);
        }
    
        private void Move()
        {
            var delta = _position - _previousPosition;
            if(Mathf.Abs(delta.magnitude) < 0.001f) { return; }
            
            OnMoved?.Invoke(_position, delta);
            if (_holding) { OnDragged?.Invoke(_position, delta); }
        }
    }
}
