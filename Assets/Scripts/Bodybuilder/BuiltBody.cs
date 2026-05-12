using UnityEngine;

namespace Bodybuilder.Bodybuilder
{
    public class BuiltBody : MonoBehaviour
    {
        private BodyPart _mainPart;
        
        [SerializeField] private float _weight;

        private void Awake()
        {
            _mainPart = GetComponent<BodyPart>();
        }

        public void UpdateStatistics()
        {
            _weight = _mainPart.TotalWeight;
        }
    }
}