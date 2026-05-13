using Bodybuilder.Bodybuilder;
using Bodybuilder.Input;
using UnityEngine;

namespace Bodybuilder
{
    public class DraggablePart : MonoBehaviour, Grabbable
    {
        private BodyPart _part;
        
        private DragLine _line;

        [SerializeField] private Vector3 _minPoint;
        [SerializeField] private Vector3 _maxPoint;

        [SerializeField] private AudioSource _attachSound;
        [SerializeField] private AudioSource _detachSound;

        [SerializeField] private float _movementSpeed = 1.0f;
        
        private ConnectionPoint _closestPoint;

        private void Awake()
        {
            _part = GetComponent<BodyPart>();
            
            _line = FindAnyObjectByType<DragLine>();
        }

        public bool Grab()
        {
            if(_part.Disconnect()) { _detachSound?.Play(); }
            return true;
        }

        public void Release()
        {
            if(_part.Connect(_closestPoint)) { _attachSound?.Play(); }
            _line.VisualizeDrag(null, null);
        }

        public Vector3 Drag(Vector3 amount)
        {
            if (_movementSpeed > 0.001f)
            {
                var newPosition = transform.position + amount * _movementSpeed;
                for (var axis = 0; axis < 3; axis++)
                {
                    newPosition[axis] = Mathf.Clamp(newPosition[axis], _minPoint[axis], _maxPoint[axis]);
                }
                transform.position = newPosition;
            }
            
            _closestPoint = _part.FindClosestConnectionPoint(transform.position);

            _line.VisualizeDrag(_part.ConnectionPoint, _closestPoint);
            
            return amount;
        }

        public void Rotate(Vector3 amount)
        {
            transform.rotation *= Quaternion.Euler(amount);
        }
    }
}
