using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThirdPersonMovement : MonoBehaviour
{
    public event Action Idle = delegate { };
    public event Action StartRunning = delegate { };
    public event Action Jumping = delegate { };
    public event Action Falling = delegate { };
    public event Action Landing = delegate { };
    public event Action StartSprinting = delegate { };

    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;

    public float speed = 6f;
    public float sprintSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float fallingThreshhold = -20f;
    public float groundDistance = 0.4f;
    public float turnSmoothTime = 0.1f;

    public LayerMask groundMask;

    float initialSpeed;
    float turnSmoothVelocity;
    bool _isMoving = false;
    bool _isJumping = false;
    bool _isFalling = false;
    bool _isSprinting = false;
    bool _isGrounded;

    Vector3 velocity;

    private void Start()
    {
        Idle?.Invoke();

        initialSpeed = speed;
    }

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            CheckIfStartedSprinting();

            speed = sprintSpeed;
        }
        else
        {
            CheckIfStoppedSprinting();

            speed = initialSpeed;
        }

        if (_isGrounded && velocity.y < 0)
        {
            CheckIfStoppedJumping();
            CheckIfStoppedFalling();

            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            CheckIfStartedJumping();

            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (velocity.y < fallingThreshhold)
        {
            CheckIfStartedFalling();
        }

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
        if (_isMoving == false && _isGrounded == true)
        {
            // our velocity says we're moving but we previously were not
            // this means we've started moving!
            StartRunning?.Invoke();
        }
        _isMoving = true;
    }

    private void CheckIfStoppedMoving()
    {
        if (_isMoving == true && _isGrounded == true)
        {
            // our velocity says we're not moving but we previously were
            // this means we've stopped!
            Idle?.Invoke();
        }
        _isMoving = false;
    }

    private void CheckIfStartedJumping()
    {
        if (_isJumping == false)
        {
            Jumping?.Invoke();
        }
        _isJumping = true;
    }

    private void CheckIfStoppedJumping()
    {
        if (_isJumping == true && _isMoving == false)
        {
            Idle?.Invoke();
        }
        else if (_isJumping == true && _isMoving == true && _isSprinting == false)
        {
            StartRunning?.Invoke();
        }
        else if (_isJumping == true && _isMoving == true && _isSprinting == true)
        {
            StartSprinting?.Invoke();
        }
        _isJumping = false;
    }

    private void CheckIfStartedFalling()
    {
        if (_isFalling == false)
        {
            Falling?.Invoke();
        }
        _isFalling = true;
    }

    private void CheckIfStoppedFalling()
    {
        if (_isFalling == true)
        {
            Landing?.Invoke();
            StartCoroutine(IsLanding());
        }
        _isFalling = false;
    }

    IEnumerator IsLanding()
    {
        yield return new WaitForSeconds(0.39f);

        if (_isMoving == false)
        {
            Idle?.Invoke();
        }
        else if (_isMoving == true && _isSprinting == false)
        {
            StartRunning?.Invoke();
        }
        else if (_isMoving == true && _isSprinting == true)
        {
            StartSprinting?.Invoke();
        }
    }

    private void CheckIfStartedSprinting()
    {
        if (_isSprinting == false && _isGrounded == true && _isMoving == true)
        {
            StartSprinting?.Invoke();
            Debug.Log("Sprinting");
        }
        _isSprinting = true;
    }

    private void CheckIfStoppedSprinting()
    {
        if (_isSprinting == true && _isGrounded == true && _isMoving == true)
        {
            StartRunning.Invoke();
            Debug.Log("Stopped Sprinting");
        }
        else if (_isSprinting == true && _isGrounded == true && _isMoving == false)
        {
            Idle?.Invoke();
        }
        _isSprinting = false;
    }
}
