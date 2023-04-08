using System.Collections;
using System.Collections.Generic;
using BasedStrategy.State;
using BasedStrategy.Unit;
using BasedStrategy.Views;
using UnityEngine;

public class LevelGridView : MonoBehaviour
{
    [SerializeField] private GridCellView _gridBoxView;

    private GridState _gridState;
    private void Awake()
    {
        _gridState = new GridState(10, 10, 2f);
        _gridState.CreateDebugGridBox(_gridBoxView);
    }

    public void SetUnitOnGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridCell gridCell = _gridState.GetGridCell(gridPosition);
        gridCell.SetUnit(unit);
    }

    public Unit GetUnitFromGridPosition(GridPosition gridPosition)
    {
        GridCell gridCell = _gridState.GetGridCell(gridPosition);
        return gridCell.CellUnit;
    }
}
