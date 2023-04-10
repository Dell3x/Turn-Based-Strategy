using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BasedStrategy.ScriptableActions
{
    [CreateAssetMenu(fileName = "Global Actions", menuName = "TurnBasedStrategy/Actions/Total Actions")]
    public class GlobalActions : ScriptableObject
    {
        [SerializeField] private GridStateActions _gridStateActions;
        [SerializeField] private UnitActions gameUnitActions;

        public GridStateActions StateActions => _gridStateActions;
        public UnitActions GameUnitActions => gameUnitActions;
    }
}