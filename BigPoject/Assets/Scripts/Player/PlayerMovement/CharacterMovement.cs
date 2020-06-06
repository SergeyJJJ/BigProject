using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CharacterMovement : MonoBehaviour
{    
    [Header("Movement controll")]
    [SerializeField] private float _horizontalSpeed = 0f;                     // How fast character can run horizontally.
    [Range(0f, 0.4f)] [SerializeField] private float _movementSmoothing = 0f; // How mouch to smouth out character movement.  

    [Space]
    [Header("Controll buttons")]
    [SerializeField] private CustomMovementButton _leftButton = null;
    [SerializeField] private CustomMovementButton _rightButton = null;

    // Events.
    public delegate void OnRun();      
    public static event OnRun onRun;                                          // Event that contains things to do when player running.
    public delegate void OnStop();                                            // Evant that contains things to do when player stops running. 
    public static event OnStop onStop;


    private const short StopMovementSpeed = 0;                                // Value indicating that the object is not moving in some direction.
    private Vector3 _currentVelocity = Vector2.zero;                          // contains curent velocity from Vector3.SmoothDump().
    private Rigidbody2D _characterRigidBody;                                  // contains character Rigidbody2d component.
    private bool _isFacingRight = false;                                       // To determine which way the player is currently facing.


    private void Awake()
    {
        InitializeRigidBodyComponents();
    }


    private void FixedUpdate()
    {
        // If appropriate buttons is enabled allow horizontal movement.
        if (IsHorizontalMovementButtonsEnabled())
        {
            HorizontalMovement();
        }

        Flip();
    }


    // Method that initialize RigidBodyComponents.
    private void InitializeRigidBodyComponents()
    {
        _characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }


    // Method that perform character movement calculation,
    // and actually move character in a horizontal direction.
    private void HorizontalMovement()
    {
        // Find target character movement velocity.
        Vector2 targetVelocity = GetHorizontalTargetVelocity();

        if (Mathf.Abs(targetVelocity.x) > 0.01f)
        {
            if (onRun != null)
            {
                onRun.Invoke();
            }
        }
        else 
        {
            if (onStop != null)
            {
                onStop.Invoke();
            }
        }
        
        SetSmoothedVelocity(targetVelocity);                             
    }


    // Smooth out velocity and apply it to the character.
    private void SetSmoothedVelocity(Vector2 targetVelocity)
    {
        _characterRigidBody.velocity = Vector3.SmoothDamp(_characterRigidBody.velocity, targetVelocity,
                                                          ref _currentVelocity, _movementSmoothing);
    }


    // Method that return horizontal target velocity based on button input.
    private Vector2 GetHorizontalTargetVelocity()
    {
        Vector2 targetVelocity = Vector2.zero;

        // Check what button is pressed and set appropriate velocity.
        if (_leftButton.IsPressed)
        {
            targetVelocity = new Vector2(-_horizontalSpeed, _characterRigidBody.velocity.y);
        }
        else if (_rightButton.IsPressed)
        {
            targetVelocity = new Vector2(_horizontalSpeed, _characterRigidBody.velocity.y);
        }
        else
        {
            targetVelocity = new Vector2(StopMovementSpeed, _characterRigidBody.velocity.y);
        }

        // Return needed velocity.
        return targetVelocity;
    }


    private void Flip()
    {
        if (_characterRigidBody.velocity.x > 0 && _isFacingRight)
        {
            RevertCharacter();
        }
        else if (_characterRigidBody.velocity.x < 0 && !_isFacingRight)
        {
            RevertCharacter();
        }
    }


    private void RevertCharacter()
    {
        // Switch the way the player is labelled as facing.
		_isFacingRight = !_isFacingRight;

        // Rotate the character 180 degrees along the Y-Axis,
        // and 0 degrees along X-Axis and Z-Axis.
		transform.Rotate(0f, 180f, 0f);
    }


    private bool IsHorizontalMovementButtonsEnabled()
    {
        return _leftButton.isActiveAndEnabled && _rightButton.isActiveAndEnabled;
    }
}