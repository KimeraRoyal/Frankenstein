using Bodybuilder.Bodybuilder;
using UnityEngine;

namespace Bodybuilder
{
    public class DragLine : MonoBehaviour
    {
        private LineRenderer _line;

        private readonly Vector3[] _points = new Vector3[2];

        private void Awake()
        {
            _line = GetComponent<LineRenderer>();
            
            _line.positionCount = 2;
            _line.enabled = false;
        }

        public void VisualizeDrag(ConnectionPoint a, ConnectionPoint b)
        {
            if (!a || !b)
            {
                _line.enabled = false;
                return;
            }
            _line.enabled = true;

            _points[0] = a.transform.position;
            _points[1] = b.transform.position;
            _line.SetPositions(_points);
        }
    }
}
