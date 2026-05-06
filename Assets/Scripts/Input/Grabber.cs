using UnityEngine;

namespace Input
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
            
            _mouse.OnPressed.AddListener(GrabAt);
            _mouse.OnReleased.AddListener(Release);
            _mouse.OnDragged += Drag;
        }

        private void GrabAt(Vector2 screenPos)
        {
            _previousPoint = _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0.0f));
            var result = Physics2D.OverlapCircle(_previousPoint, _grabRadius, _grabbableMask);   
            if(!result) { return; }

            _grabbed = result.GetComponent<Grabbable>();
            if (_grabbed != null && !_grabbed.Grab()) { _grabbed = null; }
        }

        private void Release(Vector2 screenPos)
        {
            _grabbed?.Release();
            _grabbed = null;
        }

        private Vector2 Drag(Vector2 screenPos, Vector2 screenDelta)
        {
            var point = _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0.0f));
            
            var worldDelta = point - _previousPoint;
            
            var returnDelta = worldDelta;
            if(_grabbed != null) { returnDelta = _grabbed.Drag(point - _previousPoint); }
            var diff = worldDelta - returnDelta;
            
            point -= diff;
            _previousPoint = point;

            var newScreenPos = (Vector2)_camera.WorldToScreenPoint(point);
            var screenDiff = newScreenPos - screenPos;
            if (Mathf.Abs(screenDiff.x) > 0.0001f && Mathf.Abs(screenDiff.y) > 0.0001f)
            {
                screenDelta += screenDiff;
            }
            return screenDelta;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, 0.0f), _grabRadius);
        }
    }
}
