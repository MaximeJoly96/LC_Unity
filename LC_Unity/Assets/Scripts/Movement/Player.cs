using UnityEngine;

namespace LC_Unity.Movement
{
    public class Player
    {
        private Vector2Int _gridPosition;

        public Player(int x, int y) : this(new Vector2Int(x, y)) { }

        public Player(Vector2Int gridPosition)
        {
            _gridPosition = gridPosition;
        }

        public Player() : this(0, 0) { }

        public void UpdatePosition(Vector2Int newPosition)
        {
            _gridPosition = newPosition;
        }
    }
}
