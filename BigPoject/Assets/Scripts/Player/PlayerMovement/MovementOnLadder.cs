using UnityEngine;

public class MovementOnLadder : MonoBehaviour
{
/*    [SerializeField] private float  _climbUpSpeed = 5f;                         // How fast character can clib the ladder.
    [SerializeField] private float  _climbDownSpeed = 5f;                       // How fast character will go down the stairs.
    [Range(0f, 0.4f)] [SerializeField] private float _movementSmoothing = 0f;   // How mouch to smouth out character movement.  
    [SerializeField] private CustomMovementButton _ladderClimbButton = null; 

    private Rigidbody2D _characterRigidBody = null;
    private Vector3 _currentVelocity = Vector2.zero;                            // Contains curent velocity from Vector3.SmoothDump().
    private bool _canClimb = false;                                             // Determine if player can jump a ladder.
    [SerializeField] private ScriptController _scriptController = null;

    private void Awake()
    {
        InitializeRigidBodyComponents();
    }


    private void FixedUpdate()
    {
        if (IsButtonEnabled())
        {
            if (_canClimb)
            {
                if (_ladderClimbButton.IsPressed)
                {
                    ClimbUp();
                }
                else
                {
                    ClimbDown();
                }
            }
        }
    }


    private void InitializeRigidBodyComponents()
    {
        _characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    //
    private void ClimbUp()
    {
        Vector2 targetVelocity = new Vector2(_characterRigidBody.velocity.x, _climbUpSpeed);

        _characterRigidBody.velocity = Vector3.SmoothDamp(_characterRigidBody.velocity, targetVelocity,
                                                          ref _currentVelocity, _movementSmoothing);
    }


    private void ClimbDown()
    {
        Vector2 targetVelocity = new Vector2(_characterRigidBody.velocity.x, -_climbDownSpeed);

        _characterRigidBody.velocity = targetVelocity;
    }   


    private void OnTriggerEnter2D(Collider2D other)
    {
        // If player touch the ladder, set that he can climb
        // and disable jump functionality.
        if (other.gameObject.CompareTag("Ladder"))
        {
            _canClimb = true;

            if (_scriptController != null)
            {
                _scriptController.DisableJumpScript();
            }
        }    
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        // If player touch teh ladder, set that he can`t climb
        // and enable jump functionality.
        if (other.gameObject.CompareTag("Ladder"))
        {
            _canClimb = false;

            if (_scriptController != null)
            {
                _scriptController.EnableJumpScript();
            }
        }  
    }


    private bool IsButtonEnabled()
    {
        return _ladderClimbButton != null;
    }*/
}
