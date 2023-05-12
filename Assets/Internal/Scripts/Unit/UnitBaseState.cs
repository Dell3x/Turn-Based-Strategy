using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasedStrategy.GameUnit
{
    public abstract class UnitBaseState : MonoBehaviour
    {
        private protected Unit _unit;
        private protected bool _isAnyAction;

        protected virtual void Awake()
        {
            _unit = GetComponent<Unit>();
        }
    }
}