using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThirdPersonMovement : MonoBehaviour
{
    public event Action Idle = delegate { };
    public event Action StartRunning = delegate { };

    [SerializeField] CharacterController _controller = null;
    [SerializeField] Transform _cam = null;

    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;
    bool _isMoving = false;

    private void Start()
    {
        Idle?.Invoke();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            CheckIfStartedMoving();

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            CheckIfStoppedMoving();
        }
    }

    private void CheckIfStartedMoving()
    {
        if (_isMoving == false)
        {
            // our velocity says we're moving but we previously were not
            // this means we've started moving!
            StartRunning?.Invoke();
            Debug.Log("Started");
        }
        _isMoving = true;
    }

    private void CheckIfStoppedMoving()
    {
        if (_isMoving == true)
        {
            // our velocity says we're not moving but we previously were
            // this means we've stopped!
            Idle?.Invoke();
            Debug.Log("Stopped");
        }
        _isMoving = false;
    }
}
