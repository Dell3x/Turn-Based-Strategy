

using BasedStrategy.State;

public class GridCell
{
    public GridState _gridState;
    public GridPosition _gridPosition;

    public GridCell(GridState gridState, GridPosition gridPosition)
    {
        _gridState = gridState;
        _gridPosition = gridPosition;
    }
}
