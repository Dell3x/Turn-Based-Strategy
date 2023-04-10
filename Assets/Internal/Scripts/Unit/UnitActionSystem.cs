using BasedStrategy.Mouse;
using BasedStrategy.GameUnit;
using BasedStrategy.ScriptableActions;
using UnityEngine;
using Zenject;

namespace Actions
{
    public class UnitActionSystem : MonoBehaviour
    {
        [SerializeField] private Unit _selectedUnit;
        [SerializeField] private LayerMask _unitLayerMask;

        [Inject] private GlobalActions _globalActions;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(HandleUnitSelection()) return;
                _selectedUnit.Move(MouseWorld.GetPosition());
            }
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