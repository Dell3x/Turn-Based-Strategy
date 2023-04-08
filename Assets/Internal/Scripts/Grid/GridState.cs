using UnityEngine;

namespace BasedStrategy.State
{
    public class GridState
    {
        private int _gridWidth;
        private int _gridHeight;
        private float _cellSize;
        
        public GridState(int gridWidth, int gridHeight, float cellSize)
        {
            _gridWidth = gridWidth;
            _gridHeight = gridHeight;
            _cellSize = cellSize;

            for (var x = 0; x < _gridWidth; x++)
            {
                for (var z = 0; z < _gridHeight; z++)
                {
                    
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x,z) + Vector3.right * 0.2f, Color.red, 1000);
                }
                
            }
        }

        public Vector3 GetWorldPosition(int x, int z)
        {
            return new Vector3(x, 0, z) * _cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(Mathf.RoundToInt(worldPosition.x / _cellSize), Mathf.RoundToInt(worldPosition.z / _cellSize));
        }
   
    }
}