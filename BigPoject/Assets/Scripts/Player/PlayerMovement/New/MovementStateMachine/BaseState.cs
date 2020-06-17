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
        bool isLeftButtonPressed = _characterMovement.LeftMoveButton.IsPressed;
        bool isRightButtonPressed = _characterMovement.RightMoveButton.IsPressed;

        if (isLeftButtonPressed && !isRightButtonPressed)
        {
            targetVelocity = new Vector2(-_characterMovement.HorizontalSpeed, _characterMovement.RigidBody.velocity.y);

            if(_isFacingRight)
            {
                Flip();
            }
        }
        else if (isRightButtonPressed && !isLeftButtonPressed)
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
		_isFacingRight = !_isFacingRight;

        _characterMovement.Transform.Rotate(0f, 180f, 0f);
    }
}
