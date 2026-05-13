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
        
        private ConnectionPoint _closestPoint;

        private void Awake()
        {
            _part = GetComponent<BodyPart>();
            
            _line = FindAnyObjectByType<DragLine>();
        }

        public bool Grab()
        {
            _part.Disconnect();
            return true;
        }

        public void Release()
        {
            _part.Connect(_closestPoint);
            _line.VisualizeDrag(null, null);
        }

        public Vector3 Drag(Vector3 amount)
        {
            var newPosition = transform.position + amount;
            for (var axis = 0; axis < 3; axis++)
            {
                newPosition[axis] = Mathf.Clamp(newPosition[axis], _minPoint[axis], _maxPoint[axis]);
            }
            transform.position = newPosition;
            
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
