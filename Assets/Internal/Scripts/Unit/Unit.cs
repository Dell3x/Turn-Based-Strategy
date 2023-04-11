using System;
using BasedStrategy.Mouse;
using BasedStrategy.ScriptableActions;
using BasedStrategy.Views;
using UnityEngine;
using Zenject;

namespace BasedStrategy.GameUnit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Animator _unitAnimator;
        [Header("Parameters")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        

        private const string _IsWalking = "IsWalking";
        
        private Vector3 _targetPosition;
        private GridPosition _currentGridPosition;

        [Inject] private LevelGridView _levelGridView;
        [Inject] private GlobalActions _globalActions;

        private void Awake()
        {
            _targetPosition = transform.position;
        }

        private void Start()
        {
           ChangeCurrentPosition();
        }

        private void Update()
        {
            float stoppingDistance = 0.1f;
            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
            {
                _unitAnimator.SetBool(_IsWalking, true);
                Vector3 moveDirection = (_targetPosition - transform.position).normalized;
                transform.position += moveDirection * _moveSpeed * Time.deltaTime;
                
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * _rotationSpeed);
            }
            else
            {
                _unitAnimator.SetBool(_IsWalking, false);
            }

            GridPosition newGridPosition = _levelGridView.GetGridPosition(transform.position);
            
            if (newGridPosition != _currentGridPosition)
            {
                _globalActions.StateActions.RaiseUnitChangedGridPosition(this, _currentGridPosition, newGridPosition);
                _currentGridPosition = newGridPosition;
            }

        }

        public void Move(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        private void ChangeCurrentPosition()
        {
            _currentGridPosition = _levelGridView.GetGridPosition(transform.position);
            _globalActions.StateActions.RaiseUnitSetGridPosition(_currentGridPosition, this, false);
        }
    }
}