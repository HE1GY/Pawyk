using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;

namespace Spider
{
    public class Leg : MonoBehaviour
    {
        [SerializeField] private RayThrower _rayThrower;
        [SerializeField] private Rig _rig;
        [SerializeField] private float _distanceToMove;
        [SerializeField] private float _speed;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpiderMover _spiderMover;

        private bool _isMove;
        private Transform _ikTarget;
        private Vector3 _targetPosition;
        private Vector3 _currentPosition;
        private float _currentPointOnWay;
        private Vector3 _startPos;

        [SerializeField]private float _moveOffset;


        private void Awake()
        {
            _ikTarget = _rig.GetComponentInChildren<TwoBoneIKConstraint>().data.target;
        }

        private void Start()
        {
            _currentPosition=_ikTarget.position;
        }

        private void Update()
        {
            HandleMove();
        }

        private void HandleMove()
        {
            float distance= Vector3.Distance(_ikTarget.position, _rayThrower.HitPosition);
            if (distance > _distanceToMove&&!_isMove)
            {
                _animator.SetTrigger("Move");
                _isMove = true;
                _startPos = _currentPosition;
                _targetPosition = _rayThrower.HitPosition;
                if (Vector3.Angle(_targetPosition - _startPos, _spiderMover.MoveDirection) < 30)
                {
                    _targetPosition +=_spiderMover.MoveDirection;
                }
            }
            else if (_isMove)
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
            _ikTarget.position= Vector3.Lerp( _startPos, _targetPosition, _currentPointOnWay);
            _currentPointOnWay += Time.deltaTime * _speed;
            if (_currentPointOnWay >= 1)
            {
                _currentPointOnWay = 0;
                _isMove = false;
                _currentPosition = _targetPosition;
            }
            
        }
        
    }
}
