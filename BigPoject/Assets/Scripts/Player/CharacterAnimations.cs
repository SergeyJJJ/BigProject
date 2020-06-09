using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private Sprite _defaultSprite = null;               // Contains default sprite of the character.
    [SerializeField] private Sprite _jumpSprite = null;                  // Contains jump sprite of the character.
    [SerializeField] private Animator _animator = null;                                   // Contains animator compnent of the character.
    private SpriteRenderer _spriteRenderer = null;                       // Contains spriteRenderer component of the character.


    private void Awake()
    {
        InitializeCharacterComponents();
    }


    private void InitializeCharacterComponents()
    {
        //_animator = gameObject.GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    private void OnEnable()
    {
        // Subscribe to character events 
        CharacterMovement.onRun += StartRunAnimation; 

        CharacterMovement.onStop += StopRunAnimation;
        CharacterMovement.onStop += StopLandingAnimation;

        CharacterJump.onJump += DisableAnimations;
        CharacterJump.onJump += StartJumpAnimation;

        CharacterJump.onFalling += EnableAnimations;
        CharacterJump.onFalling += StartFallingAnimation;

        CharacterJump.onLand += StartLandingAnimation;
    }

    
    private void OnDisable()
    {
        // UNsubscribe to character events 
        CharacterMovement.onRun -= StartRunAnimation; 

        CharacterMovement.onStop -= StopRunAnimation;
        CharacterMovement.onStop -= StopLandingAnimation;

        CharacterJump.onJump -= DisableAnimations;
        CharacterJump.onJump -= StartJumpAnimation;

        CharacterJump.onFalling -= EnableAnimations;
        CharacterJump.onFalling -= StartFallingAnimation;

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


    private void StopRunAnimation()
    {
        _animator.SetBool("Run", false);
    }


    private void StartJumpAnimation()
    {
        // If jump sprite is available set is to the character
        // and stop animation system.
        if (_jumpSprite != null)
        {
            _spriteRenderer.sprite = _jumpSprite; 
        }
    }


    private void StopJumpAnimation()
    {
        // If default sprite is available set to the character
        // and run animation system.
        if (_defaultSprite != null)
        {
            _spriteRenderer.sprite = _defaultSprite;
        }
    }


    private void StartFallingAnimation()
    {
        _animator.SetTrigger("Falling");
    }


    private void StartLandingAnimation()
    {
        _animator.SetBool("Landing", true);
    }


    private void StopLandingAnimation()
    {
        _animator.SetBool("Landing", false);
    }
}
