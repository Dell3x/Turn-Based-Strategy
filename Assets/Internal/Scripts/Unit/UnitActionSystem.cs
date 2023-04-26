using System;
using BasedStrategy.Mouse;
using BasedStrategy.ScriptableActions;
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

        [Inject] private GlobalActions _globalActions;
        
        private IDisposable _unitSelectionUpdate;

        private void Awake()
        {
            _unitSelectionUpdate = Observable.EveryUpdate().Subscribe(_ =>
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (HandleUnitSelection()) return;
                    _selectedUnit.Move(MouseWorld.GetPosition());
                }
            });
        }

        private void OnDisable()
        {
            _unitSelectionUpdate.Dispose();
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
            _globalActions.GameUnitActions.RaiseSelectedUnitActions(this);
        }

        public Unit GetSelectedUnit()
        {
            return _selectedUnit;
        }
    }
}