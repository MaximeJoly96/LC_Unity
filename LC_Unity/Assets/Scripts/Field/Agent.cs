using UnityEngine;

namespace Field
{
    public class Agent : MonoBehaviour
    {
        [SerializeField]
        private string _id;

        public string Id { get { return _id; } }
    }
}
