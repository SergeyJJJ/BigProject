using UnityEngine;

public abstract class BaseState
{
    protected CharacterMovement _characterMovement = null;
    protected StateMachine _stateMachine = null;

    private static bool _isFacingRight = true;

    protected BaseState(CharacterMovement characterMovement, StateMachine stateMachine)
    {
        _characterMovement = characterMovement;
        _stateMachine = stateMachine;
    }


    public virtual void Enter()
    {

    }


    public virtual void HorizontalMovement(int direction)
    {
        Debug.Log("Move");
        Vector2 targetVelocity = Vector2.zero;

        if (direction == -1)
        {
            targetVelocity = new Vector2(_characterMovement.HorizontalSpeed * direction, _characterMovement.RigidBody.velocity.y);

            if (_isFacingRight)
            {
                Flip();
            }
        }
        else if (direction == 1)
        {
            targetVelocity = new Vector2(_characterMovement.HorizontalSpeed * direction, _characterMovement.RigidBody.velocity.y);

            if (!_isFacingRight)
            {
                Flip();
            }
        }

        _characterMovement.RigidBody.velocity = Vector2.Lerp(_characterMovement.RigidBody.velocity, targetVelocity, _characterMovement.MoventSmoothing);
    }


    public virtual void RaisePlayerUp()
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
