using UnityEngine;
using System.Linq;
using System.Collections;

namespace LC_Unity.Movement
{
    public class PlayerController : MonoBehaviour
    {
        private const float TILE_SIZE_PIXELS = 32.0f;
        private const float SPEED = 4.0f;

        private Player _player;

        private Rigidbody2D _rb;
        private Vector3 _change;
        private Animator _animator;

        [SerializeField]
        private PlayerMovementAnimationSet[] _playerAnimationSet;

        private SpriteRenderer _renderer
        {
            get { return GetComponent<SpriteRenderer>(); }
        }

        private void Awake()
        {
            _player = new Player();
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            GetInput();
            HandleAnimationAndMovement();
        }

        private void UpdatePlayerOnScreen()
        {
            _rb.MovePosition(transform.position + _change * SPEED * Time.deltaTime);
        }

        private void GetInput()
        {
            _change = Vector3.zero;
            _change.x = Input.GetAxisRaw("Horizontal");
            _change.y = Input.GetAxisRaw("Vertical");
        }

        private void HandleAnimationAndMovement()
        {
            if (Vector3.Distance(_change, Vector3.zero) > 0.01f)
            {
                UpdatePlayerOnScreen();
                _animator.SetFloat("MoveX", _change.x);
                _animator.SetFloat("MoveY", _change.y);
                _animator.SetBool("Moving", true);
            }
            else
            {
                _animator.SetBool("Moving", false);
            }
        }
    }
}
