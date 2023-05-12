using System;
using UnityEngine;

namespace BasedStrategy.GameUnit
{
    public class UnitSpin : UnitBaseState
    {
        private float _currentSpinValue;

        private void Update()
        {
            if (!_isAnyAction)
            {
                return;
            }

            float spinAmount = 360 * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinAmount, 0);
            _currentSpinValue += spinAmount;

            if (_currentSpinValue >= 360f)
            {
                _isAnyAction = false;
                onActionComplete?.Invoke(false);
            }
        }

        public void SetSpinning(Action<bool> isSpineActive)
        {
            onActionComplete = isSpineActive;
            _currentSpinValue = 0;
            _isAnyAction = true;
        }
    }
}