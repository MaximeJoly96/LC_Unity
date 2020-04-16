using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LC_Unity.Movement
{
    public class PlayerController : MonoBehaviour
    {
        private Player _player;

        private void Awake()
        {
            _player = new Player();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
