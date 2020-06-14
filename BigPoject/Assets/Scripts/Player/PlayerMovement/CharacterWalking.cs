using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterWalking : MonoBehaviour
{    
    [Header("Movement controll")]
    [SerializeField] private float _horizontalSpeed = 0f;                     // How fast character can run horizontally.
    [Range(0f, 0.4f)] [SerializeField] private float _movementSmoothing = 0f; // How mouch to smouth out character movement.  

    // Events.
    public delegate void OnRun();      
    public static event OnRun onRun;                                          // Event that contains things to do when player running.
    public delegate void OnStop();                                            
    public static event OnStop onStop;                                        // Evant that contains things to do when player stops running.

    private Rigidbody2D _characterRigidBody;                                  // contains character Rigidbody2d component.
    private bool _isFacingRight = true;                                       // To determine which way the player is currently facing.

    private enum Direction
    {
        Left,
        Right
    }


    private void Awake()
    {
        InitializeRigidBodyComponent();
    }

    
    public void Move(int direction)
    {
        Vector2 targetVelocity = Vector2.zero;

        // If
        if (direction == (int)Direction.Left)
        {
            targetVelocity = new Vector2(_horizontalSpeed, _characterRigidBody.velocity.y);

            if (!_isFacingRight)
            {
                Flip();
            }
        }
        else if (direction == (int)Direction.Right)
        {
            targetVelocity = new Vector2(-_horizontalSpeed, _characterRigidBody.velocity.y);

            if (_isFacingRight)
            {
                Flip();
            }
        }

        _characterRigidBody.velocity = Vector2.Lerp(_characterRigidBody.velocity, targetVelocity, _movementSmoothing);
    }


    public void Stop()
    {
        _characterRigidBody.velocity = Vector2.zero;

        InvokeOnStop();
    }


    // Method that initialize RigidBodyComponents.
    private void InitializeRigidBodyComponent()
    {
        _characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
		_isFacingRight = !_isFacingRight;

        // Rotate the character 180 degrees along the Y-Axis,
        // and 0 degrees along X-Axis and Z-Axis.
		transform.Rotate(0f, 180f, 0f);
    }


    private void InvokeOnStop()
    {
        if (onStop != null)
        {
            onStop.Invoke();
        }
    }


    private void InvokeOnRun()
    {
        if (onRun != null)
        {
            onRun.Invoke();
        }
    }
}