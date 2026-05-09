using UnityEngine;

namespace Bodybuilder.Input
{
    public abstract class Clickable : MonoBehaviour, Grabbable
    {
        public bool Grab()
        {
            Click();
            return false;
        }

        public void Release()
        {
            
        }

        public Vector2 Drag(Vector2 amount)
            => amount;

        protected abstract void Click();
    }
}
