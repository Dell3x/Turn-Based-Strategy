using System;
using BasedStrategy.GameUnit;
using UnityEngine;

namespace BasedStrategy.ScriptableActions
{
    [CreateAssetMenu(fileName = "Grid State Actions", menuName = "TurnBasedStrategy/Actions/Grid State Actions")]
    public class GridStateActions : ScriptableObject
    {
        public Action<GridPosition, Unit, bool> OnUnitSetGridPosition;
        public Action<Unit, GridPosition, GridPosition> OnUnitChangedGridPosition;

        public void RaiseUnitSetGridPosition(GridPosition gridPosition, Unit unit, bool isRemove)
        {
            OnUnitSetGridPosition?.Invoke(gridPosition, unit, isRemove);
        }
        public void RaiseUnitChangedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
        {
            OnUnitChangedGridPosition?.Invoke(unit, fromGridPosition, toGridPosition);
        }
        

    }
}