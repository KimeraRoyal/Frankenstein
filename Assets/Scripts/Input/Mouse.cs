using System;
using UnityEngine;
using UnityEngine.Events;

namespace Input
{
    public class Mouse : MonoBehaviour
    {
        [SerializeField] private Vector2Int _screenBorder;
        
        private bool _holding;
        private Vector2 _position;
        private Vector2 _previousPosition;

        public Vector2 Position => _position;

        public UnityEvent<Vector2> OnPressed;
        public UnityEvent<Vector2> OnReleased;
    
        public Func<Vector2, Vector2, Vector2> OnDragged;

        private void Start()
        {
            _position = UnityEngine.Input.mousePosition;
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            _position += (Vector2) UnityEngine.Input.mousePositionDelta;
            _position.x = Mathf.Clamp(_position.x, _screenBorder.x, Screen.width - _screenBorder.x);
            _position.y = Mathf.Clamp(_position.y, _screenBorder.y, Screen.height - _screenBorder.y);
            
            if (_holding)
            {
                if(_holding = Hold())
                {
                    Drag();
                }
            }
            else
            {
                _holding = Press();
            }
            _previousPosition = _position;
        }

        private bool Press()
        {
            if (!UnityEngine.Input.GetMouseButtonDown(0)) { return false; }
            OnPressed?.Invoke(_position);
            return true;
        }

        private bool Hold()
        {
            if (!UnityEngine.Input.GetMouseButtonUp(0)) { return true; }
            OnReleased?.Invoke(_position);
            return false;
        }
    
        private void Drag()
        {
            var delta = _position - _previousPosition;
            if(OnDragged == null || Mathf.Abs(delta.magnitude) < 0.001f) { return; }
            var returnedDelta = OnDragged.Invoke(_position, delta);
            _position -= delta - returnedDelta;
        }
    }
}
