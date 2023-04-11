using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;
    private void Update()
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.z = + 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x = - 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.z = - 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x = + 1f;
        }

        Vector3 moveVector = Vector3.forward * moveDirection.z + Vector3.right * moveDirection.x;
        transform.position += moveVector * _moveSpeed * Time.deltaTime;

        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        transform.eulerAngles += rotationVector * _moveSpeed * 10 * Time.deltaTime;
    }
}
