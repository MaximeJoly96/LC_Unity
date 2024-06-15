using UnityEngine;
using System.Linq;
using Engine.Events;
using Inputs;

namespace Movement
{
    public class PlayerController : MonoBehaviour
    {
        private const float SPEED = 4.0f;

        private Rigidbody2D _rb;
        private Vector3 _change;
        private Animator _animator;
        private Collider2D _collider;
        private InputController _inputController;

        private SpriteRenderer _renderer
        {
            get { return GetComponent<SpriteRenderer>(); }
        }

        public Vector3 Position
        {
            get { return transform.position; }
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
            _inputController = FindObjectOfType<InputController>();

            _inputController.ButtonClicked.AddListener(HandleInput);
        }

        private void Update()
        {
            HandleAnimationAndMovement();
        }

        private void HandleInput(InputAction input)
        {
            switch(input)
            {
                case InputAction.Select:
                    CheckForInteraction();
                    break;
                case InputAction.MoveDown:
                case InputAction.MoveLeft:
                case InputAction.MoveRight:
                case InputAction.MoveUp:
                    GetInput();
                    break;
            }
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
                if (_change.x > 0.01f && _change.y > 0.01f)
                    _animator.Play("IdleUpRight");
                else if(_change.x > 0.01f && _change.y < 0.0f)
                    _animator.Play("IdleDownRight");
                else if (_change.x < 0.0f && _change.y > 0.01f)
                    _animator.Play("IdleUpLeft");
                else if (_change.x < 0.0f && _change.y < 0.0f)
                    _animator.Play("IdleDownLeft");
                else if(_change.x > 0.01f)
                    _animator.Play("IdleRight");
                else if (_change.x < 0.0f)
                    _animator.Play("IdleLeft");
                else if (_change.y > 0.01f)
                    _animator.Play("IdleUp");
                else if (_change.y < 0.0f)
                    _animator.Play("IdleDown");
            }
        }

        private void CheckForInteraction()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_collider.bounds.center, 0.2f);

            Collider2D interactible = colliders.FirstOrDefault(c => c.gameObject.GetComponent<EventsRunner>());
            if(interactible != null)
            {
                _change = interactible.transform.position - _collider.bounds.center;
                _rb.MovePosition(transform.position + _change * SPEED * Time.deltaTime);
                interactible.GetComponent<EventsRunner>().RunEvents();
            }
        }
    }
}
