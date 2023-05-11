using System;
using Actions;
using BasedStrategy.GameUnit;
using BasedStrategy.ScriptableActions;
using UnityEngine;
using Zenject;

namespace Views.BasedStrategy
{
    public class UnitSelectedView : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        [Inject] private UnitActionSystem _unitActionSystem;
        [Inject] private GlobalActions _globalActions;

        private void Start()
        {
            _globalActions.GameUnitActions.OnSelectedUnit += SelectUnit;
            UpdateVisual();
        }

        private void OnDisable()
        {
            _globalActions.GameUnitActions.OnSelectedUnit -= SelectUnit;
        }

        private void SelectUnit(object sender, EventArgs empty)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            var currentUnit = _unitActionSystem.GetSelectedUnit();
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