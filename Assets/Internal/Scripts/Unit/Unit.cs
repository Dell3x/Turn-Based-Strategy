using System;
using BasedStrategy.Mouse;
using UnityEngine;

namespace BasedStrategy.Unit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Animator _unitAnimator;
        [Header("Parameters")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        private const string _IsWalking = "IsWalking";
        
        private Vector3 _targetPosition;

        private void Awake()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            float stoppingDistance = 0.1f;
            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
            {
                _unitAnimator.SetBool(_IsWalking, true);
                Vector3 moveDirection = (_targetPosition - transform.position).normalized;
                transform.position += moveDirection * _moveSpeed * Time.deltaTime;
                
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * _rotationSpeed);
            }
            else
            {
                _unitAnimator.SetBool(_IsWalking, false);
            }

          
        }

        public void Move(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }
    }
}