using UnityEngine;

namespace Bodybuilder.Input
{
    [RequireComponent(typeof(Mouse), typeof(Camera))]
    public class Grabber : MonoBehaviour
    {
        private Mouse _mouse;
        private Camera _camera;

        private Grabbable _grabbed;
        private float _grabbedDistance;
        
        private Vector3 _previousPoint;

        [SerializeField] private float _grabRadius = 1.0f;
        [SerializeField] private LayerMask _grabbableMask;

        [SerializeField] private float _rotationWeight = 40.0f;

        private void Awake()
        {
            _mouse = GetComponent<Mouse>();
            _camera = GetComponent<Camera>();

            var lmb = _mouse.Buttons[0];
            lmb.OnPressed += GrabAt;
            lmb.OnReleased += Release;
            lmb.OnDragged += Drag;
            
            _grabbedDistance = _camera.farClipPlane;
        }

        private void GrabAt(Vector2 screenPos)
        {
            var ray = _camera.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y, _camera.farClipPlane));
            if(!Physics.SphereCast(ray, _grabRadius, out var hitInfo, _camera.farClipPlane, _grabbableMask)) { return; }
            
            _grabbed = hitInfo.collider.GetComponent<Grabbable>();
            if (_grabbed == null) { return; }
            
            if (!_grabbed.Grab())
            {
                _grabbed = null;
                return;
            }
            
            _grabbedDistance = (hitInfo.point - ray.origin).magnitude;
            _previousPoint = transform.InverseTransformPoint(hitInfo.point);
        }

        private void Release(Vector2 screenPos)
        {
            _grabbed?.Release();
            _grabbed = null;

            _grabbedDistance = _camera.farClipPlane;
        }

        private void Drag(Vector2 screenPos, Vector2 screenDelta)
        {
            if(_grabbed == null) { return; }
            var testPoint = new Vector3(screenPos.x, screenPos.y, _grabbedDistance);
            var previousPoint = testPoint - (Vector3)screenDelta;
            
            testPoint = _camera.ScreenToWorldPoint(testPoint);
            previousPoint = _camera.ScreenToWorldPoint(previousPoint);
            
            testPoint = transform.InverseTransformPoint(testPoint);
            previousPoint = transform.InverseTransformPoint(previousPoint);
            
            var delta = testPoint - previousPoint;
            delta.z = 0.0f;
            if (_mouse.MMB.Held)
            {
                var angles = new Vector3(-delta.y / _grabbedDistance, delta.x / _grabbedDistance, 0.0f) * _rotationWeight;
                _grabbed.Rotate(angles);
            }
            else
            {
                if (_mouse.RMB.Held)
                {
                    delta = new Vector3(0.0f, 0.0f, delta.y);
                    _grabbedDistance += delta.y;
                }
                delta = transform.TransformDirection(delta);
            
                _grabbed.Drag(delta);
            }
            _previousPoint = testPoint;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, 0.0f), _grabRadius);
        }
    }
}
