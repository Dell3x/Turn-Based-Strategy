using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _scrollSpeed;

    private const float _MinCameraOffset = 2f;
    private const float _MaxCameraOffset = 10f;
    private const float _DefaultSpeedOffset = 1f;

    private CinemachineTransposer _cinemachineTransposer;
    private Vector3 _followOffset;

    private void Awake()
    {
        _cinemachineTransposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _followOffset = _cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        CameraMovement();
        CameraRotation();
        CameraZoom();
    }

    private void CameraMovement()
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.z = + _DefaultSpeedOffset;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x = - _DefaultSpeedOffset;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.z = - _DefaultSpeedOffset;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x = + _DefaultSpeedOffset;
        }

        Vector3 moveVector = transform.forward * moveDirection.z + transform.right * moveDirection.x;
        transform.position += moveVector * _moveSpeed * Time.deltaTime;
    }

    private void CameraRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +_DefaultSpeedOffset;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -_DefaultSpeedOffset;
        }
        
        transform.eulerAngles += rotationVector * _moveSpeed * 10 * Time.deltaTime;
    }

    private void CameraZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            _followOffset.y -= _DefaultSpeedOffset;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            _followOffset.y += _DefaultSpeedOffset;
        }

        _followOffset.y = Math.Clamp(_followOffset.y, _MinCameraOffset, _MaxCameraOffset);
        _cinemachineTransposer.m_FollowOffset = Vector3.Lerp(_cinemachineTransposer.m_FollowOffset, _followOffset, _scrollSpeed * Time.deltaTime);
    }
}
