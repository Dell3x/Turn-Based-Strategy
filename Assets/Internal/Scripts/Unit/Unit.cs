using System;
using BasedStrategy.Mouse;
using UnityEngine;

namespace BasedStrategy.Unit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        private Vector3 _targetPosition;

        private void Update()
        {
            float stoppingDistance = 0.1f;
            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (_targetPosition - transform.position).normalized;
                transform.position += moveDirection * _moveSpeed * Time.deltaTime;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Move(MouseWorld.GetPosition());
            }
        }

        private void Move(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }
    }
}