using System;
using UnityEngine;

namespace Bodybuilding.Map.Tiles
{
    public enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }

    public static class DirectionExtensions
    {
        private const int DirectionCount = 4;

        public static Direction Rotate(this Direction direction, int rotations)
            => (Direction)(((int)direction + rotations) % DirectionCount);

        public static Direction Invert(this Direction direction)
            => Rotate(direction, 2);
        
        public static Vector2Int ToVector(this Direction direction)
            => direction switch
            {
                Direction.Left => Vector2Int.left,
                Direction.Up => Vector2Int.up,
                Direction.Right => Vector2Int.right,
                Direction.Down => Vector2Int.down,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

        public static Direction ToDirection(this Vector2Int directionVector)
        {
            if (directionVector.x != 0)
            {
                return directionVector.x < 0 ? Direction.Left : Direction.Right;
            }
            return directionVector.y < 0 ? Direction.Down : Direction.Up;
        }
        
        public static Direction ToDirection(this Vector2 directionVector)
        {
            if (Mathf.Abs(directionVector.x) > 0.0001f)
            {
                return directionVector.x < 0.0f ? Direction.Left : Direction.Right;
            }
            return directionVector.y < 0.0f ? Direction.Down : Direction.Up;
        }
        
        public static Direction ToDirection(this Vector3 directionVector)
        {
            if (Mathf.Abs(directionVector.x) > 0.0001f)
            {
                return directionVector.x < 0.0f ? Direction.Left : Direction.Right;
            }
            return directionVector.z < 0.0f ? Direction.Down : Direction.Up;
        }
    }
}