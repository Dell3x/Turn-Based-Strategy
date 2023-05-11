using System.Collections.Generic;
using BasedStrategy.State;
using BasedStrategy.GameUnit;
using BasedStrategy.ScriptableActions;
using UnityEngine;
using Zenject;

namespace BasedStrategy.Views
{
    public class LevelGridController : MonoBehaviour
    {
        [SerializeField] private GridCellView _gridBoxView;

        private GridState _gridState;

        [Inject] private GlobalActions _globalActions;
        private void Awake()
        {
            _globalActions.StateActions.OnUnitSetGridPosition += SetUnitOnGridPosition;
            _globalActions.StateActions.OnUnitChangedGridPosition += UnitChangedGridPosition;
            _gridState = new GridState(10, 10, 2f);
            _gridState.CreateDebugGridBox(_gridBoxView, this.gameObject);
        }

        private void OnDisable()
        {
            _globalActions.StateActions.OnUnitSetGridPosition -= SetUnitOnGridPosition;
            _globalActions.StateActions.OnUnitChangedGridPosition -= UnitChangedGridPosition;
        }
       
        private void SetUnitOnGridPosition(GridPosition gridPosition, Unit unit, bool isRemove)
        {
            GridCell gridCell = _gridState.GetGridCell(gridPosition);
            
            if (!isRemove)
            {
                gridCell.AddUnit(unit);
            }
            else
            {
                gridCell.RemoveUnit(unit);
            }
        }

        private List<Unit> GetUnitFromGridPosition(GridPosition gridPosition)
        {
            GridCell gridCell = _gridState.GetGridCell(gridPosition);
            return gridCell.CellUnits;
        }
        
        private void UnitChangedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
        {
             _globalActions.StateActions.RaiseUnitSetGridPosition(fromGridPosition, unit, true);
             _globalActions.StateActions.RaiseUnitSetGridPosition(toGridPosition, unit, false);
        }
        
        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return _gridState.GetGridPosition(worldPosition);
        }
        public Vector3 GetWorldGridPosition(GridPosition gridPosition)
        {
            return _gridState.GetWorldPosition(gridPosition);
        }

        public bool IsValidGridPosition(GridPosition gridPosition)
        {
            return _gridState.IsValidGridPosition(gridPosition);
        }
        
        public bool IsHasAnyUnitOnGridCell(GridPosition gridPosition)
        {
            var gridCell = _gridState.GetGridCell(gridPosition);
            return gridCell.HasAnyUnit();
        }

        public int GetWidth()
        {
            return _gridState.GetWidth();
        }

        public int GetHeight()
        {
            return _gridState.GetHeight();
        }
    }
}