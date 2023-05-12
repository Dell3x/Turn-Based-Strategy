using System;
using BasedStrategy.Mouse;
using BasedStrategy.ScriptableActions;
using BasedStrategy.Views;
using UniRx;
using UnityEngine;
using Zenject;

namespace BasedStrategy.GameUnit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitMovement _unitMovement;
        [SerializeField] private UnitSpin _unitSpin;
        
        private GridPosition _currentGridPosition;
        private IDisposable _gridPositionUpdate;

        [Inject] private LevelGridController _levelGridController;
        [Inject] private GlobalActions _globalActions;

        private void Start()
        {
           ChangeCurrentPosition();
           
          _gridPositionUpdate = Observable.EveryUpdate().Subscribe(_ =>
           {
               GridPosition newGridPosition = _levelGridController.GetGridPosition(transform.position);

               if (newGridPosition != _currentGridPosition)
               {
                   _globalActions.StateActions.RaiseUnitChangedGridPosition(this, _currentGridPosition,
                       newGridPosition);
                   _currentGridPosition = newGridPosition;
               }
           });
        }

        private void OnDisable()
        {
            _gridPositionUpdate.Dispose();
        }

        public UnitMovement GetUnitMovement()
        {
            return _unitMovement;
        }

        public UnitSpin GetUnitSpin()
        {
            return _unitSpin;
        }

        public GridPosition GetGridPosition()
        {
            return _currentGridPosition;
        }

        private void ChangeCurrentPosition()
        {
            _currentGridPosition = _levelGridController.GetGridPosition(transform.position);
            _globalActions.StateActions.RaiseUnitSetGridPosition(_currentGridPosition, this, false);
        }
    }
}