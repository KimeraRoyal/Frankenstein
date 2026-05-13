using System;
using Bodybuilder.Bodybuilder;
using TMPro;
using UnityEngine;

namespace Bodybuilder
{
    public class PointMeter : MonoBehaviour
    {
        private BuiltBody _body;
        
        [SerializeField] private TMP_Text _label;
        [SerializeField] private string _labelFormat = "{0}/{1}";
        
        [SerializeField] private Transform _meter;
        [SerializeField] private float _minRotation, _maxRotation;

        [SerializeField] private float _fillSmoothTime = 1.0f;
        private float _currentFill = 0.0f;
        private float _targetFill = 0.0f;
        private float _fillVelocity = 0.0f;
        
        private void Awake()
        {
            _body = FindAnyObjectByType<BuiltBody>();
            _body.OnStatisticsUpdated += StatisticsUpdated;
        }

        private void Update()
        {
            if(Mathf.Abs(_targetFill - _currentFill) < 0.001f) { return; }
            _currentFill = Mathf.SmoothDamp(_currentFill, _targetFill, ref _fillVelocity, _fillSmoothTime);
            _meter.eulerAngles = Vector3.forward * Mathf.Lerp(_minRotation, _maxRotation, _currentFill);
        }

        private void StatisticsUpdated()
        {
            _label.text = string.Format(_labelFormat, _body.Cost, _body.MaxCost);

            _targetFill = _body.CostSpentPercentage;
        }
    }
}
