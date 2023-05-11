using System;
using System.Collections.Generic;
using Actions;
using BasedStrategy.ScriptableActions;
using BasedStrategy.Views;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Unit = BasedStrategy.GameUnit.Unit;

public class GridVisualController : MonoBehaviour
{
    [SerializeField] private GameObject _gridVisualPrefab;
    [SerializeField] private Transform _parentTransform;

    private GridVisualView[,] _gridVisualViews;
    private IDisposable _gridVisualUpdate;

    [Inject] private LevelGridController _levelGridController;
    [Inject] private UnitActionSystem _unitActionSystem;

    private void Awake()
    {
        GenerateVisualCells();
        _gridVisualUpdate = Observable.EveryUpdate().Subscribe(_ => { OnUpdateVisuals(); });
    }

    private void OnDisable()
    {
        _gridVisualUpdate.Dispose();
    }

    private void GenerateVisualCells()
    {
        _gridVisualViews = new GridVisualView[_levelGridController.GetWidth(), _levelGridController.GetHeight()];
        
        for (var x = 0; x < _levelGridController.GetWidth(); x++)
        {
            for (var z = 0; z < _levelGridController.GetHeight(); z++)
            {
                var gridPosition = new GridPosition(x, z);
                var gridVisualObject = Instantiate(_gridVisualPrefab, _levelGridController.GetWorldGridPosition(gridPosition), quaternion.identity, _parentTransform);
                _gridVisualViews[x, z] = gridVisualObject.GetComponent<GridVisualView>();
            }
        }
    }

    private void HideAllGridVisuals()
    {
        for (var x = 0; x < _levelGridController.GetWidth(); x++)
        {
            for (var z = 0; z < _levelGridController.GetHeight(); z++)
            {
                _gridVisualViews[x, z].SetActivation(false);
            }
        }
    }

    private void ShowGridPositionList(List<GridPosition> gridPositions)
    {
        foreach (var position in gridPositions)
        {
            _gridVisualViews[position._xPosition, position._zPosition].SetActivation(true);
        }
    }

    private void OnUpdateVisuals()
    {
        var currentUnit = _unitActionSystem.GetSelectedUnit();
        HideAllGridVisuals();
        ShowGridPositionList(currentUnit.GetUnitMovement().GetValidGridPosition());
    }
}
