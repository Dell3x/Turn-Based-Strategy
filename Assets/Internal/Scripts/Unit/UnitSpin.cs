using UnityEngine;

namespace BasedStrategy.GameUnit
{
    public class UnitSpin : UnitBaseState
    {
        private float _currentSpinValue;

        private void Update()
        {
            if (_isAnyAction)
            {
                float spinAmount = 360 * Time.deltaTime;
                transform.eulerAngles += new Vector3(0, spinAmount, 0);
                _currentSpinValue += spinAmount;
                
                if (_currentSpinValue >= 360f)
                {
                    SetSpinning(false);
                }
            }
        }

        public void SetSpinning(bool isSpinning)
        {
            _currentSpinValue = 0;
            _isAnyAction = isSpinning;
        }
    }
}