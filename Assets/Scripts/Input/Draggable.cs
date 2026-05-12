using UnityEngine;

namespace Bodybuilder.Input
{
    public class Draggable : MonoBehaviour, Grabbable
    {
        [SerializeField] private float _maxMovement = -1.0f;
        
        public bool Grab() { return true; }

        public void Release() { }

        public Vector3 Drag(Vector3 amount)
        {
            if (_maxMovement > 0.0f)
            {
                amount.x = Mathf.Min(amount.x, _maxMovement);
                amount.y = Mathf.Min(amount.y, _maxMovement);
            }
            transform.localPosition += amount;
            return amount;
        }
    }
}
