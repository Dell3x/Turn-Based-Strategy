using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BasedStrategy.Views
{
    public class GridCellView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gridText;

        private GridCell _gridCell;

        public void SetGridCell(GridCell gridCell)
        {
            _gridCell = gridCell;
            _gridText.text = $"{gridCell._gridPosition}";

        }
        
    }
}