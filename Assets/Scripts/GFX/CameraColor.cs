using UnityEngine;

namespace GFX
{
    public class CameraColor : MonoBehaviour
    {
        private Camera _camera;

        [SerializeField] private Color[] _colors;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        public void SetColor(int index)
            => _camera.backgroundColor = _colors[index];
    }
}