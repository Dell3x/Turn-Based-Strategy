using System.Collections.Generic;
using BasedStrategy.State;
using BasedStrategy.GameUnit;

namespace BasedStrategy.GameUnit
{
    public class GridCell
    {
        public GridState _gridState;
        public GridPosition _gridPosition;
        public List<Unit> _units;

        public List<Unit> CellUnits => _units;

        public GridCell(GridState gridState, GridPosition gridPosition)
        {
            _gridState = gridState;
            _gridPosition = gridPosition;
            _units = new List<Unit>();
        }

        public void AddUnit(Unit unit)
        {
            _units.Add(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            _units.Remove(unit);
        }

        public bool HasAnyUnit()
        {
            return _units.Count > 0;
        }
    }
}