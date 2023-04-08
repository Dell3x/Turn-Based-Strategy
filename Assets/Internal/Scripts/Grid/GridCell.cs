

using BasedStrategy.State;
using BasedStrategy.Unit;

public class GridCell
{
    public GridState _gridState;
    public GridPosition _gridPosition;
    public Unit _unit;

    public Unit CellUnit => _unit;

    public GridCell(GridState gridState, GridPosition gridPosition)
    {
        _gridState = gridState;
        _gridPosition = gridPosition;
    }

    public void SetUnit(Unit unit)
    {
        _unit = unit;
    }
}
