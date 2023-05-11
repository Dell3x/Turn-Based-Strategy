using System;
using BasedStrategy.GameUnit;
using UnityEngine;

namespace BasedStrategy.ScriptableActions
{
    [CreateAssetMenu(fileName = "Unit Actions", menuName = "TurnBasedStrategy/Actions/Unit Actions")]

    public class UnitActions : ScriptableObject
    {
        public EventHandler OnSelectedUnit;

        public void RaiseSelectedUnitActions(object sender)
        {
            OnSelectedUnit?.Invoke(sender, EventArgs.Empty);
        }
    }
}