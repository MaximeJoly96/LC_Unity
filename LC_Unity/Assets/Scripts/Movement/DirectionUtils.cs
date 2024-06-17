using UnityEngine;
using Logging;

namespace Movement
{
    public enum Direction
    {
        Left, Right, Up, Down, UpLeft, UpRight, DownLeft, DownRight
    }

    public static class DirectionUtils
    {
        public static Direction VectorToDirection(Vector2 vector)
        {
            vector.Normalize();

            if(vector.x < 0.0f)
            {
                if (Mathf.Abs(vector.y - 0.0f) < 0.01f)
                    return Direction.Left;
                else if (vector.y > 0.0f)
                    return Direction.UpLeft;
                else
                    return Direction.DownLeft;
            }
            else if(vector.x > 0.0f)
            {
                if (Mathf.Abs(vector.y - 0.0f) < 0.01f)
                    return Direction.Right;
                else if (vector.y > 0.0f)
                    return Direction.UpRight;
                else
                    return Direction.DownRight;
            }
            else if(Mathf.Abs(vector.x - 0.0f) < 0.01f)
            {
                return vector.y < 0.0f ? Direction.Down : Direction.Up;
            }

            LogsHandler.Instance.LogError("Could not compute direction from vector.");
            return Direction.Down;
        }

        public static Direction GetOppositeDirection(Direction direction)
        {
            switch(direction)
            {
                case Direction.Down:
                    return Direction.Up;
                case Direction.DownLeft:
                    return Direction.UpRight;
                case Direction.DownRight:
                    return Direction.UpLeft;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Up:
                    return Direction.Down;
                case Direction.UpLeft:
                    return Direction.DownRight;
                case Direction.UpRight:
                    return Direction.DownLeft;
                default:
                    LogsHandler.Instance.LogError("Could not get opposite direction because the provided direction is undefined.");
                    return Direction.Down;
            }
        }

        public static Vector2 DirectionToNormalizedVector(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return new Vector2(0.0f, -1.0f);
                case Direction.DownLeft:
                    return new Vector2(-1.0f, -1.0f);
                case Direction.DownRight:
                    return new Vector2(1.0f, -1.0f);
                case Direction.Left:
                    return new Vector2(-1.0f, 0.0f);
                case Direction.Right:
                    return new Vector2(1.0f, 0.0f);
                case Direction.Up:
                    return new Vector2(0.0f, 1.0f);
                case Direction.UpLeft:
                    return new Vector2(-1.0f, 1.0f);
                case Direction.UpRight:
                    return new Vector2(1.0f, 1.0f);
                default:
                    LogsHandler.Instance.LogError("Could not get normalized vector because the provided direction is undefined.");
                    return Vector2.zero;
            }
        }

        public static Direction ApplyAngleToDirection(int angle, Direction direction)
        {
            int baseAngle = DirectionToAngle(direction);

            baseAngle += angle;

            return AngleToDirection(baseAngle);
        }

        /// <summary>
        /// This is considering the regular trigonometry circle. So right is a 0 degree angle.
        /// </summary>
        /// <param name="direction">The provided direction, mapped to the trigonometry circle.</param>
        /// <returns>The angle in degrees based on the provided direction.</returns>
        public static int DirectionToAngle(Direction direction)
        {
            switch(direction)
            {
                case Direction.Down:
                    return 270;
                case Direction.DownLeft:
                    return 225;
                case Direction.DownRight:
                    return 315;
                case Direction.Left:
                    return 180;
                case Direction.Right:
                    return 0;
                case Direction.Up:
                    return 90;
                case Direction.UpLeft:
                    return 135;
                case Direction.UpRight:
                    return 45;
                default:
                    LogsHandler.Instance.LogError("Could not get angle from direction because the provided direction is undefined.");
                    return 0;
            }
        }

        /// <summary>
        /// This is considering the regular trigonometry circle. So 0 degree points to the right.
        /// </summary>
        /// <param name="angle">The provided angle in degrees.</param>
        /// <returns>The direction which is the most accurate based on the provided angle.</returns>
        public static Direction AngleToDirection(int angle)
        {
            if (angle < 23 && angle > 337)
                return Direction.Right;
            else if (angle >= 23 && angle < 68)
                return Direction.UpRight;
            else if (angle >= 68 && angle < 113)
                return Direction.Up;
            else if (angle >= 113 && angle < 158)
                return Direction.UpLeft;
            else if (angle >= 158 && angle < 203)
                return Direction.Left;
            else if (angle >= 203 && angle < 248)
                return Direction.DownLeft;
            else if (angle >= 248 && angle < 293)
                return Direction.Down;
            else
                return Direction.DownRight;
        }
    }
}
