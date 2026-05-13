using System;
using UnityEngine;

namespace Bodybuilder.Bodybuilder
{
    public class BuiltBody : MonoBehaviour
    {
        private BodyPart _mainPart;

        [SerializeField] private int _maxCost = 100;
        private int _cost;
        
        private float _weight;

        private int _footCount;
        private float _movementSpeed;

        private int _handCount;
        private float _climbingSpeed;

        private PartFeatures _features;

        public int MaxCost => _maxCost;
        public int Cost => _cost;
        public float CostSpentPercentage => Mathf.Clamp01((float) Cost / MaxCost);
        
        public float Weight => _weight;

        public bool HasFeet => _footCount > 0;
        public int FootCount => _footCount;
        public float MovementSpeed => _movementSpeed;

        public bool HasHands => _handCount > 0;
        public int HandCount => _handCount;
        public float ClimbingSpeed => _climbingSpeed;

        public PartFeatures Features => _features;
        
        public Action OnStatisticsUpdated;
        
        private void Awake()
        {
            _mainPart = GetComponent<BodyPart>();
        }

        private void Start()
        {
            UpdateStatistics();
        }

        public void UpdateStatistics()
        {
            _cost = _mainPart.TotalCost;
            
            _weight = _mainPart.TotalWeight;

            _footCount = _mainPart.TotalFootCount;
            _movementSpeed = _mainPart.TotalMovementSpeed;
            
            _handCount = _mainPart.TotalHandCount;
            _climbingSpeed = _mainPart.TotalClimbingSpeed;

            _features = _mainPart.TotalFeatures;
            
            OnStatisticsUpdated?.Invoke();
        }

        public bool CanAddPoints(int amount)
            => _cost + amount <= _maxCost;
    }
}