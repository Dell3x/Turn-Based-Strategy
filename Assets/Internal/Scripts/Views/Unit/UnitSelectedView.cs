using System;
using Actions;
using BasedStrategy.Unit;
using UnityEngine;

namespace Views.BasedStrategy
{
    public class UnitSelectedView : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private MeshRenderer _meshRenderer;

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnit += SelectUnit;
            UpdateVisual();
        }

        private void OnDisable()
        {
            UnitActionSystem.Instance.OnSelectedUnit -= SelectUnit;
        }

        private void SelectUnit(object sender, EventArgs empty)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            var currentUnit = UnitActionSystem.Instance.GetSelectedUnit();
            if (currentUnit == _unit)
            {
                _meshRenderer.enabled = true;
            }
            else
            {
                _meshRenderer.enabled = false;
            }
        }
        
    }
}