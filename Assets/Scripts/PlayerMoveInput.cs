using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveInput : MonoBehaviour
{

    private InputAction _moveAction = null;
    private InputAction _JumpAcion = null;
    private CharacterController _characterController = null;
    public float _gravityFactorJumpUp = 1;

    public PlayerInput _PlayerInput = null;

    public Animator _animator;

    int jumpHash = Animator.StringToHash("jump");
    int slideHash = Animator.StringToHash("slide");

    public float actionDuration = 1f;
    private float actionTimeRemaining = 0f;


    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        //_moveAction = _PlayerInput.actions.FindAction("Move", true);
        _JumpAcion = _PlayerInput.actions.FindAction("Jump", true);

        _animator.SetFloat("speed", 1.0f);
    }

    public void OnJump()
    {
        if (actionTimeRemaining <= 0 && _characterController.isGrounded())
        {
            Debug.Log("---------------------------------- startJump");
            _characterController.jump();
            _animator.SetTrigger(jumpHash);
            actionTimeRemaining = actionDuration;
        }
    }

    public void OnSlide()
    {
        if (actionTimeRemaining <= 0)
        {
            _animator.SetTrigger(slideHash);
            actionTimeRemaining = actionDuration;
        }

    }

    public void OnLeft()
    {
        Debug.Log("test");
        if(actionTimeRemaining <= 0 && _characterController.moveLeft())
        {
            _animator.SetTrigger("left");
            actionTimeRemaining = actionDuration;
        }
    }

    public void OnRight()
    {
        if(actionTimeRemaining <= 0 && _characterController.moveRight())
        {
            _animator.SetTrigger("right");
            actionTimeRemaining = actionDuration;
        }
    }

    private void Update()
    {

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);


        if (actionTimeRemaining > 0)
        {
            actionTimeRemaining -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        //Retrieve player input
        //Vector2 inputMove = _moveAction.ReadValue<Vector2>();
        float inputJump = _JumpAcion.ReadValue<float>();


        if (inputJump > 0.5f)
        {
            //_characterController.setCustomGravityFactor(_gravityFactorJumpUp);
        }
    }



}