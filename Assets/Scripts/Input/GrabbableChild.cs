using UnityEngine;

namespace Input
{
    public class GrabbableChild : MonoBehaviour, Grabbable
    {
        private Grabbable _parent;

        private void Awake()
        {
            _parent = transform.parent?.GetComponentInParent<Grabbable>();
        }

        public bool Grab()
            => _parent.Grab();

        public void Release()
            => _parent.Release();

        public Vector2 Drag(Vector2 amount)
            => _parent.Drag(amount);
    }
}
