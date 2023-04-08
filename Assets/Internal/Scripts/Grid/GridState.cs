using BasedStrategy.Views;
using UnityEngine;

namespace BasedStrategy.State
{
    public class GridState
    {
        private int _gridWidth;
        private int _gridHeight;
        private float _cellSize;
        private GridCell[,] _gridBoxes;
        
        public GridState(int gridWidth, int gridHeight, float cellSize)
        {
            _gridWidth = gridWidth;
            _gridHeight = gridHeight;
            _cellSize = cellSize;
            _gridBoxes = new GridCell[_gridWidth, _gridHeight];

            for (var x = 0; x < _gridWidth; x++)
            {
                for (var z = 0; z < _gridHeight; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                   _gridBoxes[x,z] = new GridCell(this, gridPosition);
                }
                
            }
        }

        public Vector3 GetWorldPosition(GridPosition gridPosition)
        {
            return new Vector3(gridPosition._xPosition, 0, gridPosition._zPosition) * _cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(Mathf.RoundToInt(worldPosition.x / _cellSize), Mathf.RoundToInt(worldPosition.z / _cellSize));
        }

        public void CreateDebugGridBox(GridCellView gridCellView)
        {
            for (var x = 0; x < _gridWidth; x++)
            {
                for (var z = 0; z < _gridHeight; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    var currentGridCellView = GameObject.Instantiate(gridCellView, GetWorldPosition(gridPosition), Quaternion.identity);
                    currentGridCellView.SetGridCell(GetGridCell(gridPosition));
                }
            }
        }

        public GridCell GetGridCell(GridPosition gridPosition)
        {
            return _gridBoxes[gridPosition._xPosition, gridPosition._zPosition];
        }
   
    }
}