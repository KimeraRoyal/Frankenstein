using UnityEngine;

namespace Bodybuilder.Map.Selection.Selectables
{
    [RequireComponent(typeof(Collider))]
    public class MapSelectableCollider : MonoBehaviour
    {
        private IMapSelectable _selectable;

        public IMapSelectable Selectable => _selectable;
        
        private void Awake()
        {
            _selectable = GetComponentInParent<IMapSelectable>();
        }
    }
}