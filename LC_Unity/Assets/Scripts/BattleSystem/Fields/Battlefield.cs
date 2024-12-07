using UnityEngine;

namespace BattleSystem.Fields
{
    public class Battlefield : MonoBehaviour
    {
        [SerializeField]
        private int _id;

        public int Id { get { return _id; } set { _id = value; } }
    }
}
