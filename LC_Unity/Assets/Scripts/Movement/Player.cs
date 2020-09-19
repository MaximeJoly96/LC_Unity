using UnityEngine;

namespace LC_Unity.Movement
{
    public enum PlayerDirection { Bottom, Left, Top, Right }

    public class Player
    {
        private Vector2 _gridPosition;
        private PlayerDirection _direction;

        public Vector2 GridPosition { get { return _gridPosition; } }
        public PlayerDirection Direction { get { return _direction; } }

        public Player(int x, int y) : this(new Vector2Int(x, y)) { }

        public Player(Vector2Int gridPosition)
        {
            _gridPosition = gridPosition;
            _direction = PlayerDirection.Bottom;
        }

        public Player() : this(0, 0) { }

        public void UpdatePosition(Vector2 newPosition)
        {
            _direction = GetDirectionFromCurrentAndNewPosition(newPosition, _gridPosition);
            _gridPosition = newPosition;
        }

        private PlayerDirection GetDirectionFromCurrentAndNewPosition(Vector2 newPos, Vector2 oldPos)
        {
            Vector2 diff = (newPos - oldPos).normalized;
            PlayerDirection direction = PlayerDirection.Bottom;

            if(Vector2.Distance(diff, new Vector2Int(1, 0)) < 0.01f)
            {
                direction = PlayerDirection.Right;
            }
            else if (Vector2.Distance(diff, new Vector2Int(-1, 0)) < 0.01f)
            {
                direction = PlayerDirection.Left;
            }
            else if (Vector2.Distance(diff, new Vector2Int(0, 1)) < 0.01f)
            {
                direction = PlayerDirection.Top;
            }
            else if (Vector2.Distance(diff, new Vector2Int(0, -1)) < 0.01f)
            {
                direction = PlayerDirection.Bottom;
            }

            return direction;
        }
    }
}
