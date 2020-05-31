using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 400;                 // Character jump force.
    private Rigidbody2D _characterRigidBody = null;                         // Hold character Rigidbody2d component.


    private void Start()
    {
        InitializeRigidBodyComponents();
    }

    private void FixedUpdate()
    {

    }


    // Method that initialize RigidBodyComponents.
    private void InitializeRigidBodyComponents()
    {
        _characterRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }


    // Method that allows the character to jump.
    public void Jump()
    {
        float zeroForce = 0f;

        // Direction in which raise the character.
        Vector2 jumpDirection = new Vector2(zeroForce, _jumpForce);

        // Apply force up to raise the character.
        _characterRigidBody.AddForce(jumpDirection, ForceMode2D.Force);
    }
/*
    private bool IsGrounded()
    {

    }*/
}
