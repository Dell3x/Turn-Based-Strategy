using System;
using BasedStrategy.Mouse;
using BasedStrategy.ScriptableActions;
using BasedStrategy.Views;
using UniRx;
using UnityEngine;
using Zenject;
using Unit = BasedStrategy.GameUnit.Unit;

namespace Actions
{
    public class UnitActionSystem : MonoBehaviour
    {
        [SerializeField] private Unit _selectedUnit;
        [SerializeField] private LayerMask _unitLayerMask;

        private bool _isUnitBusy;
        private IDisposable _unitSelectionUpdate;

        [Inject] private GlobalActions _globalActions;
        [Inject] private LevelGridController _levelGridController;

        private void Awake()
        {
            _unitSelectionUpdate = Observable.EveryUpdate().Subscribe(_ =>
            {
                if (_isUnitBusy)
                {
                    return;
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    if (HandleUnitSelection())
                    {
                        return;
                    }
                    
                    var mouseGridPosition = _levelGridController.GetGridPosition(MouseWorld.GetPosition());
                    if (_selectedUnit.GetUnitMovement().IsMovingForValidPosition(mouseGridPosition))
                    {
                        SetUnitBusy(true);
                        _selectedUnit.GetUnitMovement().Move(mouseGridPosition, SetUnitBusy);
                    }
                }

                if (Input.GetMouseButtonDown(1))
                {
                    SetUnitBusy(true);
                    _selectedUnit.GetUnitSpin().SetSpinning(SetUnitBusy);
                }
            });
        }

        private void OnDisable()
        {
            _unitSelectionUpdate.Dispose();
        }

        private void SetUnitBusy(bool isBusy)
        {
            _isUnitBusy = isBusy;
            Debug.Log(_isUnitBusy);
        }

        private bool HandleUnitSelection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }

            return false;
        }

        private void SetSelectedUnit(Unit unit)
        {
            _selectedUnit = unit;
            _globalActions.GameUnitActions.RaiseSelectedUnitActions();
        }

        public Unit GetSelectedUnit()
        {
            return _selectedUnit;
        }
    }
}