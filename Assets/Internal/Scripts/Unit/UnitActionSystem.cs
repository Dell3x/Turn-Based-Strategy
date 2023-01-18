using System;
using BasedStrategy.Mouse;
using BasedStrategy.Unit;
using UnityEngine;

namespace Actions
{
    public class UnitActionSystem : MonoBehaviour
    {
        public static UnitActionSystem Instance;
        
        public EventHandler OnSelectedUnit;
    
        [SerializeField] private Unit _selectedUnit;
        [SerializeField] private LayerMask _unitLayerMask;


        private void Awake()
        {
            if (Instance = null)
            {
                return;
            }
            Instance = this;
        }

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
            OnSelectedUnit?.Invoke(this, EventArgs.Empty);
        }

        public Unit GetSelectedUnit()
        {
            return _selectedUnit;
        }
    }
}