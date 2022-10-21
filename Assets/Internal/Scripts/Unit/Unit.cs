using System;
using UnityEngine;

namespace BasedStrategy.Unit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        private Vector3 _targetPosition;

        private void Update()
        {
            var stoppingDistance = 0.1f;
            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (_targetPosition - transform.position).normalized;
                transform.position += moveDirection * _moveSpeed * Time.deltaTime;
                
                if (Input.GetKeyDown(KeyCode.D))
                {
                    Move(new Vector3(4,0,4));
                }
            }
        }

        private void Move(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }
    }
}