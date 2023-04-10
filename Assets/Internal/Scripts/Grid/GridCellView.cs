using System;
using BasedStrategy.GameUnit;
using BasedStrategy.ScriptableActions;
using TMPro;
using UnityEngine;
using Zenject;

namespace BasedStrategy.Views
{
    public class GridCellView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gridText;

        private GridCell _gridCell;

        [Inject] private GlobalActions _globalActions;

        public void SetGridCell(GridCell gridCell)
        {
            _gridCell = gridCell;
        }

        private void Update()
        {
            UpdateCellTextInfo();
        }

        private void UpdateCellTextInfo()
        {
            _gridText.text = $"{_gridCell._gridPosition} \n {_gridCell.CellUnit?.name}";
        }
    }
}