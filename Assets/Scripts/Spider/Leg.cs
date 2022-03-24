using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Spider
{
    public class Leg : MonoBehaviour
    {
        [SerializeField] private RayThrower _rayThrower;
        [SerializeField] private Rig _rig;
        [SerializeField] private float _distanceToMove;
        [SerializeField] private float _speed;
        [SerializeField] private Animator _animator;
        


        private Transform _ikTarget;
        private Vector3 _targetPosition;
        private Vector3 _currentPosition;
        private bool _move;
        private float _currentPointOnWay;
        private Vector3 _startPos;


        private void Awake()
        {
            _ikTarget = _rig.GetComponentInChildren<TwoBoneIKConstraint>().data.target;
        }

        private void Start()
        {
            _currentPosition=_rayThrower.HitPosition;
        }

        private void Update()
        {
            _targetPosition = _rayThrower.HitPosition;
            HandleMove();
        }

        private void HandleMove()
        {
            float distance= Vector3.Distance(_ikTarget.position, _targetPosition);
            if (distance > _distanceToMove&&!_move)
            {
                _animator.SetTrigger("Move");
                _move = true;
                _startPos = _currentPosition;

            }
            else if (_move)
            {
                Move();
            }
            else 
            {
                _ikTarget.position = _currentPosition;
            }
        }

        private  void Move()
        {
            _ikTarget.position= Vector3.Lerp(_startPos, _targetPosition, _currentPointOnWay);
            _currentPointOnWay += Time.deltaTime * _speed;
            if (_currentPointOnWay >= 1)
            {
                _currentPointOnWay = 0;
                _move = false;
                _currentPosition = _targetPosition;
            }
            
        }
        
    }
}
