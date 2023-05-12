using System;
using BasedStrategy.GameUnit;
using UnityEngine;

namespace BasedStrategy.ScriptableActions
{
    [CreateAssetMenu(fileName = "Unit Actions", menuName = "TurnBasedStrategy/Actions/Unit Actions")]

    public class UnitActions : ScriptableObject
    {
        public Action OnSelectedUnit;

        public void RaiseSelectedUnitActions()
        {
            OnSelectedUnit?.Invoke();
        }
    }
}