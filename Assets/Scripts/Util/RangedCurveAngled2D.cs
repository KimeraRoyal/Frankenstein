using System;
using UnityEngine;

namespace Bodybuilder.Util
{
    [Serializable]
    public class RangedCurveAngled2D
    {
        [SerializeField] private RangedCurveAngled _x;
        [SerializeField] private RangedCurveAngled _y;

        public RangedCurveAngled X => _x;
        public RangedCurveAngled Y => _y;

        public ParticleSystem.MinMaxCurve Evaluate(float t, float angle)
            => new(_x.Evaluate(t, angle), _y.Evaluate(t, angle));
    }
}