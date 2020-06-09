using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;                  // Contains animator compnent of the character.


    private void Awake()
    {
        InitializeCharacterComponents();
        Time.timeScale = 0.05f;
    }


    private void InitializeCharacterComponents()
    {
        //_animator = gameObject.GetComponent<Animator>();
    }


    private void OnEnable()
    {
        // Subscribe to character events 
        CharacterMovement.onRun += StartRunAnimation; 

        CharacterMovement.onStop += StartIdleAnimation;

        CharacterJump.onJump += StartJumpAnimation;

        CharacterJump.onFalling += StartFallAnimation;

        CharacterJump.onLand += StartLandingAnimation;
    }

    
    private void OnDisable()
    {
        // Unsubscribe to character events 
        CharacterMovement.onRun -= StartRunAnimation; 

        CharacterMovement.onStop -= StartIdleAnimation;

        CharacterJump.onJump -= StartJumpAnimation;

        CharacterJump.onFalling -= StartFallAnimation;

        CharacterJump.onLand -= StartLandingAnimation;
    }


    private void EnableAnimations()
    {
        _animator.enabled = true;
    }


    private void DisableAnimations()
    {
        _animator.enabled = false;
    }


    private void StartRunAnimation()
    {
        _animator.SetBool("Run", true);
    }


    private void StartIdleAnimation()
    {
        _animator.SetBool("Run", false);
    }


    private void StartJumpAnimation()
    {
        _animator.SetTrigger("Jump");
    }


    private void StartFallAnimation()
    {
        _animator.SetBool("Fall", true);
    }


    private void StartLandingAnimation()
    {
        _animator.SetBool("Fall", false);
    }
}
