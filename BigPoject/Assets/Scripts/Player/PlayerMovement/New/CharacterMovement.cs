using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Horizontal movement")]
    [SerializeField] private float _horizontalSpeed = 0;
    [SerializeField] private float _movementSmothing = 0;

    [Header("Vertical movement")]
    [SerializeField] private float _jumpHeight = 0;
    [SerializeField] private float  _climbUpSpeed = 0f;

    [Header("Is on ground controll")]
    [SerializeField] private Transform _groundCheckPoint = null;
    [SerializeField] private float _groundCheckRadius = 0f;
    [SerializeField] private LayerMask _whatIsGround = Physics2D.AllLayers;


    private StateMachine _stateMachine = null;
    private JumpingState _jumpingState = null;
    private IdleState _idleState = null;
    private RunningState _runningState = null;
    private FallingState _fallingState = null;
    private LandingState _landingState = null;
    private LadderClimdingState _ladderClimbingState = null;

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

    public float  ClimbUpSpeed
    {
        get
        {
            return _climbUpSpeed;
        }
    }

    public Transform GroundCheckPoint
    {
        get
        {
            return _groundCheckPoint;
        }
    }

    public float GrounCheckRadius
    {
        get
        {
            return _groundCheckRadius;
        }
    }

    public LayerMask WhatIsGround
    {
        get
        {
            return _whatIsGround;
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

    public JumpingState Jumping
    {
        get
        {
            return _jumpingState;
        }
    }

    public IdleState Idle
    {
        get
        {
            return _idleState;
        }
    }

    public RunningState Running
    {
        get
        {
            return _runningState;
        }
    }

    public FallingState Falling
    {
        get
        {
            return _fallingState;
        }
    }

    public LandingState Landing
    {
        get
        {
            return _landingState;
        }
    }
    public LadderClimdingState LadderClimbing
    {
        get
        {
            return _ladderClimbingState;
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
        _runningState = new RunningState(this, _stateMachine);
        _fallingState = new FallingState(this, _stateMachine);
        _landingState = new LandingState(this, _stateMachine);
        _ladderClimbingState = new LadderClimdingState(this, _stateMachine);

        _stateMachine.Initialization(Idle); 
    }

    
    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
    }


    public void HorizontalMovement(int direction)
    {
        _stateMachine.CurrentState.HorizontalMovement(direction);
    }


    public void StopHorizontalMovement()
    {
        _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
    }


    public void Jump()
    {
        _stateMachine.CurrentState.RaisePlayerUp();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        _stateMachine.CurrentState.OnTriggerEnter2D(other);    
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        _stateMachine.CurrentState.OnTriggerExit2D(other);
    }
}