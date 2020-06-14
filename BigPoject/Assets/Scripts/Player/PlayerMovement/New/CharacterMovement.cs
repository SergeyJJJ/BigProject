using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Horizontal movement")]
    [SerializeField] private float _horizontalSpeed = 0;
    [SerializeField] private float _movementSmothing = 0;

    [Header("Vertical movement")]
    [SerializeField] private float _jumpHeight = 0;

    private StateMachine _stateMachine = null;
    private JumpingState _jumpingState = null;
    private IdleState _idleState = null;

    private Transform _transform = null;
    private Rigidbody2D _rigidBody = null;

    #region Properties
    public float HorizontalSpeed
    {
        get
        {
            return _horizontalSpeed;
        }
    }

    public float MoventSmoothing
    {
        get
        {
            return _movementSmothing;
        }
    }

    public float JumpHeight
    {
        get
        {
            return _jumpHeight;
        }
    }

    public Transform Transform
    {
        get
        {
            return _transform;
        }
    }

    public Rigidbody2D RigidBody
    {
        get
        {
            return _rigidBody;
        }
    }

    public StateMachine StateMachine
    {
        get
        {
            return _stateMachine;
        }
    }

    public JumpingState JumpingState
    {
        get
        {
            return _jumpingState;
        }
    }

    public IdleState IdleState
    {
        get
        {
            return _idleState;
        }
    }
    #endregion Properties

    private void Start()
    {
        _transform = transform;
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();

        _stateMachine = new StateMachine();
        _jumpingState = new JumpingState(this, _stateMachine);
        _idleState = new IdleState(this, _stateMachine);    

        _stateMachine.Initialization(IdleState); 
    }

    public void HorizontalMovement(int direction)
    {
        _stateMachine.CurrentState.HorizontalMovement(direction);
    }

    public void Stop()
    {
        _rigidBody.velocity = Vector2.zero;
    }

    public void Jump()
    {
        _stateMachine.CurrentState.RaisePlayerUp();
    }

}