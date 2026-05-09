using System;
using UnityEngine;

namespace Bodybuilder.Util
{
    [Serializable]
    public class RangedCurve2D
    {
        [SerializeField] private RangedCurve _x;
        [SerializeField] private RangedCurve _y;

        public RangedCurve X => _x;
        public RangedCurve Y => _y;

        public ParticleSystem.MinMaxCurve Evaluate(float t)
            => new(_x.Evaluate(t), _y.Evaluate(t));
    }
}