using UnityEngine;
using System.Linq;
using Engine.Events;
using Inputs;
using Field;
using Core;
using System.Collections.Generic;
using Mobile;
using Engine.Movement;

namespace Movement
{
    public class PlayerController : MonoBehaviour
    {
        private static float SPEED
        {
            get
            {
#if UNITY_ANDROID
                return 2.0f;
#endif
                return 4.0f;
            }
        }

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
#if UNITY_ANDROID
            _inputController.NoMovement.AddListener(StopMovement);
#endif
        }

        private void FixedUpdate()
        {
            CheckMapTransitions();
        }

        private void Update()
        {
            HandleAnimationAndMovement();
            CheckForContactEvents();
        }

        private void HandleInput(InputAction input)
        {
            if (GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.OnField)
            {
                switch (input)
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
            else
                _change = Vector3.zero;
        }

        private void UpdatePlayerOnScreen()
        {
            _rb.MovePosition(transform.position + SPEED * Time.deltaTime * _change);
        }

        private void GetInput()
        {
            _change = Vector3.zero;
#if UNITY_ANDROID
            Vector2 currentDirection = FindObjectOfType<MobileDPad>().CurrentDirection;
            _change = currentDirection;
#else
            _change.x = Input.GetAxisRaw("Horizontal");
            _change.y = Input.GetAxisRaw("Vertical");
#endif
        }

        private void HandleAnimationAndMovement()
        {
            if (Vector3.Distance(_change, Vector3.zero) > 0.01f)
            {
                UpdatePlayerOnScreen();
                _animator.SetFloat("X", _change.x);
                _animator.SetFloat("Y", _change.y);

                _animator.SetBool("Moving", true);
            }
            else
                _animator.SetBool("Moving", false);
        }

        private void CheckMapTransitions()
        {
            if (Vector2.Distance(_change, Vector2.zero) < 0.01f)
                return;

            RaycastHit2D[] hits = Physics2D.RaycastAll(GetComponent<Collider2D>().bounds.center, _change);

            RaycastHit2D transition = hits.FirstOrDefault(h => h && h.collider.GetComponent<MapTransition>() && h.distance < 0.1f);

            if (transition)
                transition.collider.GetComponent<MapTransition>().TriggerTransition();
        }

        public void CheckForInteraction()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_collider.bounds.center, 0.2f);

            Collider2D interactible = colliders.FirstOrDefault(c => c.gameObject.GetComponent<RunnableAgent>());
            if(interactible != null && interactible.GetComponent<RunnableAgent>().Trigger == AgentTrigger.Manual)
            {
                SpriteRenderer playerSr = GetComponent<SpriteRenderer>();
                SpriteRenderer interactSr = interactible.GetComponent<SpriteRenderer>();

                _change = interactible.transform.position - transform.position;
                HandleAnimationAndMovement();
                _rb.velocity = Vector3.zero;
                _change = Vector3.zero;
                GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.Interacting);

                RunnableAgent agent = interactible.GetComponent<RunnableAgent>();
                agent.UpdateDirection(DirectionUtils.VectorToDirection(transform.position - interactible.transform.position));

                agent.FinishedSequence.AddListener(() => GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OnField));
                agent.RunSequence();
            }
        }

        private void CheckForContactEvents()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_collider.bounds.center, 0.05f);

            Collider2D interactible = colliders.FirstOrDefault(c => c.gameObject.GetComponent<RunnableAgent>());
            if(interactible != null && interactible.GetComponent<RunnableAgent>().Trigger == AgentTrigger.Contact)
            {
                RunnableAgent agent = interactible.GetComponent<RunnableAgent>();
                agent.FinishedSequence.AddListener(() => GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.OnField));
                agent.RunSequence();
            }
        }

        public void ForceDirection(TransferObject.PossibleDirection direction)
        {
            _change = Vector3.zero;

            _animator.SetFloat("X", _change.x);
            _animator.SetFloat("Y", _change.y);
            _animator.SetBool("Moving", false);
            _animator.Play("Idle");

            switch(direction)
            {
                case TransferObject.PossibleDirection.Top:
                    _animator.SetFloat("Y", 1.0f);
                    break;
                case TransferObject.PossibleDirection.Bottom:
                    _animator.SetFloat("Y", -1.0f);
                    break;
                case TransferObject.PossibleDirection.Left:
                    _animator.SetFloat("X", -1.0f);
                    break;
                case TransferObject.PossibleDirection.Right:
                    _animator.SetFloat("X", 1.0f);
                    break;
                case TransferObject.PossibleDirection.Retain:
                    break;
            }
        }

        private void StopMovement()
        {
            _change = Vector3.zero;
            _animator.SetBool("Moving", false);
        }
    }
}
