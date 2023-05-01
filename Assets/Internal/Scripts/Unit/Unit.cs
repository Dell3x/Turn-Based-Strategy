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
        
        private GridPosition _currentGridPosition;
        private IDisposable _gridPositionUpdate;

        [Inject] private LevelGridView _levelGridView;
        [Inject] private GlobalActions _globalActions;

        private void Start()
        {
           ChangeCurrentPosition();
           
          _gridPositionUpdate = Observable.EveryUpdate().Subscribe(_ =>
           {
               GridPosition newGridPosition = _levelGridView.GetGridPosition(transform.position);

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

        public GridPosition GetGridPosition()
        {
            return _currentGridPosition;
        }

        private void ChangeCurrentPosition()
        {
            _currentGridPosition = _levelGridView.GetGridPosition(transform.position);
            _globalActions.StateActions.RaiseUnitSetGridPosition(_currentGridPosition, this, false);
        }
    }
}