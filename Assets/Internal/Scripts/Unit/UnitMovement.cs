using System;
using System.Collections.Generic;
using BasedStrategy.Views;
using UniRx;
using UnityEngine;
using Zenject;

namespace BasedStrategy.GameUnit
{
    public class UnitMovement : UnitBaseState
    {
        [SerializeField] private Animator _unitAnimator;

        [Header("Parameters")] 
        [SerializeField] private int _maxMoveDistance;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        [Inject] private LevelGridController _levelGridController;

        private const string _IsWalking = "IsWalking";

        private Vector3 _targetPosition;
        private IDisposable _unitMovementUpdate;


        protected override void Awake()
        {
            base.Awake();
            _targetPosition = transform.position;
            _unitMovementUpdate = Observable.EveryUpdate().Subscribe(_ => { UnitDirectionalMovement(); });
        }

        private void OnDisable()
        {
            _unitMovementUpdate.Dispose();
        }

        public void Move(GridPosition targetGridPosition, Action<bool> onMovingActive)
        {
            onActionComplete = onMovingActive;
            _targetPosition = _levelGridController.GetWorldGridPosition(targetGridPosition);
            _isAnyAction = true;
        }

        public bool IsMovingForValidPosition(GridPosition gridPosition)
        {
            List<GridPosition> validGridPositions = GetValidGridPosition();
            return validGridPositions.Contains(gridPosition);
        }

        public List<GridPosition> GetValidGridPosition()
        {
            List<GridPosition> validGridPositions = new List<GridPosition>();
            var unitGridPosition = _unit.GetGridPosition();

            for (var x = -_maxMoveDistance; x < +_maxMoveDistance; x++)
            {
                for (var z = -_maxMoveDistance; z <= _maxMoveDistance; z++)
                {
                    GridPosition gridPositionOffset = new GridPosition(x, z);
                    GridPosition currentGridPosition = unitGridPosition + gridPositionOffset;

                    if (!_levelGridController.IsValidGridPosition(currentGridPosition))
                    {
                        continue;
                    }

                    if (unitGridPosition == currentGridPosition)
                    {
                        continue;
                    }

                    if (_levelGridController.IsHasAnyUnitOnGridCell(currentGridPosition))
                    {
                        continue;
                    }

                    validGridPositions.Add(currentGridPosition);
                }
            }

            return validGridPositions;
        }

        private void UnitDirectionalMovement()
        {
            if (!_isAnyAction)
            {
                return;
            }
            
            float stoppingDistance = 0.1f;
            Vector3 moveDirection = (_targetPosition - transform.position).normalized;
            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
            {
                _unitAnimator.SetBool(_IsWalking, true);
                transform.position += moveDirection * _moveSpeed * Time.deltaTime;
            }
            else
            {
                _isAnyAction = false;
                onActionComplete?.Invoke(false);
                _unitAnimator.SetBool(_IsWalking, false);
            }
            
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * _rotationSpeed);

        }
    }
}