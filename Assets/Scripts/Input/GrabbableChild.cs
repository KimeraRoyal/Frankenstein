using UnityEngine;

namespace Bodybuilder.Input
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

        public Vector3 Drag(Vector3 amount)
            => _parent.Drag(amount);
    }
}
