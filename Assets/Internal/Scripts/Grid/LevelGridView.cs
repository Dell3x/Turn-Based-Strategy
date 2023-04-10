using System;
using BasedStrategy.State;
using BasedStrategy.GameUnit;
using BasedStrategy.ScriptableActions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace BasedStrategy.Views
{
    public class LevelGridView : MonoBehaviour
    {
        [SerializeField] private GridCellView _gridBoxView;

        private GridState _gridState;

        [Inject] private GlobalActions _globalActions;
        private void Awake()
        {
            _globalActions.StateActions.OnUnitSetGridPosition += SetUnitOnGridPosition;
            _globalActions.StateActions.OnUnitChangedGridPosition += UnitChangedGridPosition;
            _gridState = new GridState(10, 10, 2f);
            _gridState.CreateDebugGridBox(_gridBoxView);
        }

        private void OnDisable()
        {
            _globalActions.StateActions.OnUnitSetGridPosition -= SetUnitOnGridPosition;
            _globalActions.StateActions.OnUnitChangedGridPosition -= UnitChangedGridPosition;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return _gridState.GetGridPosition(worldPosition);
        }

        private void SetUnitOnGridPosition(GridPosition gridPosition, Unit unit)
        {
            GridCell gridCell = _gridState.GetGridCell(gridPosition);
            gridCell.SetUnit(unit);
        }

        private Unit GetUnitFromGridPosition(GridPosition gridPosition)
        {
            GridCell gridCell = _gridState.GetGridCell(gridPosition);
            return gridCell.CellUnit;
        }

        private void UnitChangedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
        {
             _globalActions.StateActions.RaiseUnitSetGridPosition(fromGridPosition, null);
             _globalActions.StateActions.RaiseUnitSetGridPosition(toGridPosition, unit);
        }
    }
}