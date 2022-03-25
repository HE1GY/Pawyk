using System;
using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Spider
{
    public class SpiderMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _body;
        [SerializeField]private float _max;
        [SerializeField]private float _min;

        private PlayerInput _input;
        private Vector2 _inputDirection;
        public Vector3 MoveDirection { get; private set; }
        
        
        private void Awake()
        {
            _input = new PlayerInput();

            _input.Spider.Move.started += OnMoveInput;
            _input.Spider.Move.performed += OnMoveInput;
            _input.Spider.Move.canceled += OnMoveInput;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }


        private void Update()
        {
            HandleMove();
            HandleVerticalMove();
        }

        private void OnMoveInput(InputAction.CallbackContext ctx)
        {
            _inputDirection = ctx.ReadValue<Vector2>();
        }


        private void HandleMove()
        {
            MoveDirection = (_body.right * _inputDirection.y - _body.forward * _inputDirection.x);
            transform.Translate(MoveDirection*_speed*Time.deltaTime);
        }

        private void HandleVerticalMove()
        {
            Vector3 rayOrg = transform.position;
            Vector3 rayDir = -_body.up;
            Ray ray = new Ray(rayOrg, rayDir);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 100, LayerMask.GetMask("Ground")))
            {
                if (Vector3.Distance(raycastHit.point, transform.position) > _max)
                {
                    transform.Translate(rayDir*Time.deltaTime);
                }
                else if (Vector3.Distance(raycastHit.point, transform.position) < _min)
                {
                    transform.Translate(-rayDir*Time.deltaTime);
                }
                
            }

        }
        
    }
}
