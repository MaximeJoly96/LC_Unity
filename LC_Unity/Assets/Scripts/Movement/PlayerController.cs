using UnityEngine;
using System.Linq;
using LC_Unity.Interactions;

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
        private Collider2D _collider;

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
            _collider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            GetInput();
            CheckForInteraction();
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

        private void CheckForInteraction()
        {
            if(Input.GetButtonDown("Select"))
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(_collider.bounds.center, 0.2f);

                Collider2D interactible = colliders.FirstOrDefault(c => c.gameObject.GetComponent<InteractibleTerrainElement>());
                if(interactible != null)
                {
                    _change = interactible.transform.position - _collider.bounds.center;
                    _rb.MovePosition(transform.position + _change * SPEED * Time.deltaTime);
                    interactible.GetComponent<InteractibleTerrainElement>().Interact(this.gameObject);
                }
            }
        }
    }
}
