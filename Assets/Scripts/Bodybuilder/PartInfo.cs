using UnityEngine;

namespace Bodybuilder.Bodybuilder
{
    [CreateAssetMenu(fileName = "Body Part Info", menuName = "Bodybuilding/Body Part Info")]
    public class PartInfo : ScriptableObject
    {
        [SerializeField] private int _pointCost = 1;
        
        [SerializeField] private float _weight = 1.0f;

        [SerializeField] private bool _isFoot;
        [SerializeField] private float _movementSpeed;

        [SerializeField] private bool _isHand;
        [SerializeField] private float _climbingSpeed;

        [SerializeField] private PartFeatures _features;

        public int PointCost => _pointCost;

        public float Weight => _weight;

        public bool IsFoot => _isFoot;
        public float MovementSpeed => _movementSpeed;

        public bool IsHand => _isHand;
        public float ClimbingSpeed => _climbingSpeed;

        public PartFeatures Features => _features;
    }
}