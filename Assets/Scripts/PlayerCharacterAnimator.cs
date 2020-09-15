using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCharacterAnimator : MonoBehaviour
{
    [SerializeField] ThirdPersonMovement _thirdPersonMovement = null;
    
    // these names align with the naming in our Animator node
    const string IdleState = "Idle";
    const string RunState = "Run";
    const string JumpState = "Jumping";
    const string FallState = "Falling";
    const string LandState = "Landing";
    const string SprintState = "Sprint";

    Animator _animator = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnIdle()
    {
        _animator.CrossFadeInFixedTime(IdleState, .2f);
    }

    private void OnStartRunning()
    {
        _animator.CrossFadeInFixedTime(RunState, .2f);
    }

    private void OnJumping()
    {
        _animator.CrossFadeInFixedTime(JumpState, .2f);
    }

    private void OnFalling()
    {
        _animator.CrossFadeInFixedTime(FallState, .2f);
    }

    private void OnLanding()
    {
        _animator.CrossFadeInFixedTime(LandState, .2f);
    }

    private void OnSprinting()
    {
        _animator.CrossFadeInFixedTime(SprintState, .2f);
    }

    private void OnEnable()
    {
        _thirdPersonMovement.Idle += OnIdle;
        _thirdPersonMovement.StartRunning += OnStartRunning;
        _thirdPersonMovement.Jumping += OnJumping;
        _thirdPersonMovement.Falling += OnFalling;
        _thirdPersonMovement.Landing += OnLanding;
        _thirdPersonMovement.StartSprinting += OnSprinting;
    }

    private void OnDisable()
    {
        _thirdPersonMovement.Idle -= OnIdle;
        _thirdPersonMovement.StartRunning -= OnStartRunning;
        _thirdPersonMovement.Jumping -= OnJumping;
        _thirdPersonMovement.Falling -= OnFalling;
        _thirdPersonMovement.Landing -= OnLanding;
        _thirdPersonMovement.StartSprinting -= OnSprinting;
    }
}
