using UnityEngine;

public abstract class BaseState
{
    protected CharacterMovement _characterMovement = null;
    protected StateMachine _stateMachine = null;

    private Vector2 _currentVelocity = Vector2.zero;
    private static bool _isFacingRight = true;
    private static float _direction = 0;

    protected BaseState(CharacterMovement characterMovement, StateMachine stateMachine)
    {
        _characterMovement = characterMovement;
        _stateMachine = stateMachine;
    }


    public virtual void Enter()
    {

    }


    public virtual void HorizontalInput(int direction)
    {
        _direction = direction;
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
        Vector2 targetVelocity = Vector2.zero;

        targetVelocity = new Vector2(_characterMovement.HorizontalSpeed * _direction, _characterMovement.RigidBody.velocity.y);
        
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
