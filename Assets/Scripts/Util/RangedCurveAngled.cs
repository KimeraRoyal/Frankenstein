using System;
using UnityEngine;

namespace Bodybuilder.Util
{
    [Serializable]
    public class RangedCurveAngled
    {
        [SerializeField] private RangedCurve _up;
        [SerializeField] private RangedCurve _right;
        [SerializeField] private RangedCurve _down;
        [SerializeField] private RangedCurve _left;

        public float Evaluate(float t, float angle)
        {
            var quadrantPercent = angle / 90.0f % 1.0f;
            return angle switch
            {
                < 90.0f => Mathf.Lerp(_up.Evaluate(t), _left.Evaluate(t), quadrantPercent),
                < 180.0f => Mathf.Lerp(_left.Evaluate(t), _down.Evaluate(t), quadrantPercent),
                < 270.0f => Mathf.Lerp(_down.Evaluate(t), _right.Evaluate(t), quadrantPercent),
                _ => Mathf.Lerp(_right.Evaluate(t), _up.Evaluate(t), quadrantPercent)
            };
        }
    }
}