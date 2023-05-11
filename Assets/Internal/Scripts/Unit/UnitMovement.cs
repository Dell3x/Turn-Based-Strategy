using System;
using System.Collections.Generic;
using Actions;
using BasedStrategy.ScriptableActions;
using BasedStrategy.Views;
using UniRx;
using UnityEngine;
using Zenject;

namespace BasedStrategy.GameUnit
{
    public class UnitMovement : MonoBehaviour
    {
        [SerializeField] private Animator _unitAnimator;
        [SerializeField] private Unit _unit;

        [Header("Parameters")] 
        [SerializeField] private int _maxMoveDistance;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        [Inject] private LevelGridController _levelGridController;
        [Inject] private GlobalActions _globalActions;
        [Inject] private UnitActionSystem _unitActionSystem;

        private const string _IsWalking = "IsWalking";

        private Vector3 _targetPosition;
        private IDisposable _unitMovementUpdate;


        private void Awake()
        {
            _targetPosition = transform.position;
            _unitMovementUpdate = Observable.EveryUpdate().Subscribe(_ => { UnitDirectionalMovement(); });
        }

        private void OnDisable()
        {
            _unitMovementUpdate.Dispose();
        }

        public void Move(GridPosition targetGridPosition)
        {
            _targetPosition = _levelGridController.GetWorldGridPosition(targetGridPosition);
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
        }
    }
}