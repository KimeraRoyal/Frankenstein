using UnityEngine;

namespace Bodybuilder.Input
{
    [RequireComponent(typeof(Mouse), typeof(Camera))]
    public class Grabber : MonoBehaviour
    {
        private Mouse _mouse;
        private Camera _camera;

        private Vector3 _previousPoint;
        private Grabbable _grabbed;

        [SerializeField] private float _grabRadius = 1.0f;
        [SerializeField] private LayerMask _grabbableMask;

        private void Awake()
        {
            _mouse = GetComponent<Mouse>();
            _camera = GetComponent<Camera>();
            
            _mouse.OnPressed += GrabAt;
            _mouse.OnReleased += Release;
            _mouse.OnDragged += Drag;
        }

        private void GrabAt(Vector2 screenPos)
        {
            var ray = _camera.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y, 5.0f));
            if(!Physics.SphereCast(ray, _grabRadius, out var hitInfo, _grabbableMask)) { return; }
            _grabbed = hitInfo.collider.GetComponent<Grabbable>();
            if (_grabbed != null && !_grabbed.Grab()) { _grabbed = null; }
        }

        private void Release(Vector2 screenPos)
        {
            _grabbed?.Release();
            _grabbed = null;
        }

        private void Drag(Vector2 screenPos, Vector2 screenDelta)
        {
            
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, 0.0f), _grabRadius);
        }
    }
}
