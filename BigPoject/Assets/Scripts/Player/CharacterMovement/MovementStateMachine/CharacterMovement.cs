﻿using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Horizontal movement controll")]
    [SerializeField] private float _horizontalSpeed = 0f;                     // Character`s horizontal speed.
    [Range(0f, 0.4f), SerializeField] private float _movementSmothing = 0f;   // Coefficient of character`s horizontal movement smoothing.
    [SerializeField] private CustomButton _leftMoveButton = null;             // Left movement button.     
    [SerializeField] private CustomButton _rightMoveButton = null;            // Right movement  button.

    [Header("Jump controll")]
    [SerializeField] private float _jumpHeight = 0f;                          // Character`s jump height.
    [SerializeField] private float _afterGroundTouchJumpTime = 0f;            // Time during which character still can jump after he touch ground last time.
    [SerializeField] private float _pressBeforeGroundTime = 0f;               // Time furing which character can perform jump after he press jump button last time.
    [ Range(0f, 1f), SerializeField] private float _cutJumpHeight = 0;        // Coefficient that determine how much jump will be cut, after player relesase jump button.
    [SerializeField] private CustomButton _upMoveButton = null;               // Jump and climb button.

    [Header("Ladder movement controll")]
    [SerializeField] private float  _climbUpSpeed = 0f;                       // Climb up speed.
    [SerializeField] private float _climbDownSpeed = 0f;                      // Climb down speed.

    [Header("Is on ground controll")]
    [SerializeField] private SurfaceCheck _surfaceCheck = null;               // Script that check if character is on ground.

    // Movement states.
    private StateMachine _stateMachine = null;                                // Script that controll transition between states.
    private JumpingState _jumpingState = null;
    private IdleState _idleState = null;
    private RunningState _runningState = null;
    private FallingState _fallingState = null;
    private LandingState _landingState = null;
    private ClimbingState _climbingState = null;

    private Transform _transform = null;                                      // Characters Transform component.
    private Rigidbody2D _rigidBody = null;                                    // Charecters Rigidbody2D component.

    private float _afterGoundTouchTimer = 0;                                  // Timer that controlls time during which character still can jump after he touch ground last time.
    private float _pressButtonTimer = 0;                                      // Timer that controlls time furing which character can perform jump after he press jump button last time.

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

    public CustomButton LeftMoveButton
    {
        get
        {
            return _leftMoveButton;
        }
    }

    public CustomButton RightMoveButton
    {
        get
        {
            return _rightMoveButton;
        }
    }

    public float JumpHeight
    {
        get
        {
            return _jumpHeight;
        }
    }

    public float AfterGroundTouchJumpTime
    {
        get
        {
            return _afterGroundTouchJumpTime;
        }
    }

    public float PressBeforeGroundTime
    {
        get
        {
            return _pressBeforeGroundTime;
        }
    }

    public float CutJumpHeight
    {
        get
        {
            return _cutJumpHeight;
        }
    }

    public CustomButton UpMoveButton
    {
        get
        {
            return _upMoveButton;
        }
    }

    public float  ClimbUpSpeed
    {
        get
        {
            return _climbUpSpeed;
        }
    }

    public float ClimbDownSpeed
    {
        get
        {
            return _climbDownSpeed;
        }
    }

    public SurfaceCheck SurfaceCheck
    {
        get
        {
            return _surfaceCheck;
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

    public ClimbingState Climbing
    {
        get
        {
            return _climbingState;
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

    public float AfterGoundTouchTimer
    {
        get
        {
            return _afterGoundTouchTimer;
        }

        set
        {
            _afterGoundTouchTimer = value;
        }
    }

    public float PressButtonTimer
    {
        get
        {
            return _pressButtonTimer;
        }

        set
        {
            _pressButtonTimer = value;
        }    
    }
    #endregion Properties

    private void Start()
    {
        // Initialize character`s components.
        _transform = transform;
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();

        // Initialize state machine and states.
        _stateMachine = new StateMachine();
        _jumpingState = new JumpingState(this, _stateMachine);
        _idleState = new IdleState(this, _stateMachine);    
        _runningState = new RunningState(this, _stateMachine);
        _fallingState = new FallingState(this, _stateMachine);
        _landingState = new LandingState(this, _stateMachine);
        _climbingState = new ClimbingState(this, _stateMachine);

        // Set first state.
        _stateMachine.Initialization(Idle); 
    }

    
    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
        Debug.Log(_stateMachine.CurrentState);
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