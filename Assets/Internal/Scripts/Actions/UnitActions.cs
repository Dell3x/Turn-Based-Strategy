using System;
using UnityEngine;

namespace BasedStrategy.ScriptableActions
{
    [CreateAssetMenu(fileName = "Unit Actions", menuName = "TurnBasedStrategy/Actions/Unit Actions")]

    public class UnitActions : ScriptableObject
    {
        public EventHandler OnSelectedunit;

        public void RaiseSelectedUnitActions(object sender)
        {
            OnSelectedunit?.Invoke(sender, EventArgs.Empty);
        }
    }
}