using UnityEngine;

public abstract class BaseState
{
    protected CharacterMovement _characterMovement = null;
    protected StateMachine _stateMachine = null;

    private Vector2 _currentVelocity = Vector2.zero;
    private static bool _isFacingRight = true;
    
    private bool _moveLeft = false;
    private bool _moveRight = false;

    protected BaseState(CharacterMovement characterMovement, StateMachine stateMachine)
    {
        _characterMovement = characterMovement;
        _stateMachine = stateMachine;
    }


    public virtual void Enter()
    {

    }


    public virtual void LeftMovementInput(bool moveLeft)
    {
        _moveLeft = moveLeft;
    }


    public virtual void RightMovementInput(bool moveRight)
    {
        _moveRight = moveRight;
    }


    public virtual void RaisePlayerUpInput(int direction)
    {

    }


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        
    }


    public virtual void OnTriggerExit2D(Collider2D other)
    {

    }


    public virtual void PhysicsUpdate()
    {
        Vector2 targetVelocity = new Vector2(0, _characterMovement.RigidBody.velocity.y);

        if (_moveLeft)
        {
            targetVelocity = new Vector2(-_characterMovement.HorizontalSpeed, _characterMovement.RigidBody.velocity.y);

            if(_isFacingRight)
            {
                Flip();
            }
        }
        else if (_moveRight)
        {
            targetVelocity = new Vector2(_characterMovement.HorizontalSpeed, _characterMovement.RigidBody.velocity.y);

            if(!_isFacingRight)
            {
                Flip();
            }
        }

        _characterMovement.RigidBody.velocity = Vector2.SmoothDamp(_characterMovement.RigidBody.velocity,
                                                                   targetVelocity, ref _currentVelocity,
                                                                   _characterMovement.MoventSmoothing);
    }


    public virtual void Exit()
    {

    }

    private void Flip()
    {
        Debug.Log("work");
        // Switch the way the player is labelled as facing.
		_isFacingRight = !_isFacingRight;

        // Rotate the character 180 degrees along the Y-Axis,
        // and 0 degrees along X-Axis and Z-Axis.
		_characterMovement.Transform.Rotate(0f, 180f, 0f);
    }
}
