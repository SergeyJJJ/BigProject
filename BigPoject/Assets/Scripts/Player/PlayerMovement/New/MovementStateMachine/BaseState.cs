using UnityEngine;

public abstract class BaseState
{
    protected CharacterMovement _characterMovement = null;
    protected StateMachine _stateMachine = null;

    private Vector2 _currentVelocity = Vector2.zero;
    private static bool _isFacingRight = true;

    protected BaseState(CharacterMovement characterMovement, StateMachine stateMachine)
    {
        _characterMovement = characterMovement;
        _stateMachine = stateMachine;
    }


    public virtual void Enter()
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

        if (_characterMovement.LeftMoveButton.IsPressed)
        {
            targetVelocity = new Vector2(-_characterMovement.HorizontalSpeed, _characterMovement.RigidBody.velocity.y);

            if(_isFacingRight)
            {
                Flip();
            }
        }
        else if (_characterMovement.RightMoveButton.IsPressed)
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
        // Switch the way the player is labelled as facing.
		_isFacingRight = !_isFacingRight;

        // Rotate the character 180 degrees along the Y-Axis,
        // and 0 degrees along X-Axis and Z-Axis.
		_characterMovement.Transform.Rotate(0f, 180f, 0f);
    }
}
