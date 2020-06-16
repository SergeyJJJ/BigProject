using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Horizontal movement")]
    [SerializeField] private float _horizontalSpeed = 0f;
    [Range(0f, 0.4f), SerializeField] private float _movementSmothing = 0f;

    [Header("Vertical movement")]
    [SerializeField] private float _jumpHeight = 0f;
    [SerializeField] private float _afterGroundTouchJumpTime = 0f;
    [SerializeField] private float _pressBeforeGroundTime = 0f;  
    [ Range(0f, 1f), SerializeField] private float _cutJumpHeight = 0;
    [SerializeField] private float  _climbUpSpeedLadder = 0f;

    [Header("Is on ground controll")]
    [SerializeField] private SurfaceCheck _surfaceCheck = null;

    private StateMachine _stateMachine = null;
    private JumpingState _jumpingState = null;
    private IdleState _idleState = null;
    private RunningState _runningState = null;
    private FallingState _fallingState = null;
    private LandingState _landingState = null;
    private LadderClimdingState _ladderClimbingState = null;

    private Transform _transform = null;
    private Rigidbody2D _rigidBody = null;

    private float _afterGoundTouchTimer = 0;
    private float _pressButtonTimer = 0;

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

    public float  ClimbUpSpeed
    {
        get
        {
            return _climbUpSpeedLadder;
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
    public LadderClimdingState LadderClimbing
    {
        get
        {
            return _ladderClimbingState;
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
        Debug.Log(_afterGoundTouchTimer);
        _stateMachine.CurrentState.PhysicsUpdate();
    }


    public void HorizontalMovementInput(int direction)
    {
        _stateMachine.CurrentState.HorizontalMovementInput(direction);
    }


    public void RaisePlayerUpInput(int direction)
    {
        _stateMachine.CurrentState.RaisePlayerUpInput(direction);
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