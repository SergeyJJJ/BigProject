using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private Sprite _defaultSprite = null;
    [SerializeField] private Sprite _jumpSprite = null;
    private Animator _animator = null;
    private SpriteRenderer _spriteRenderer = null;


    private void Awake()
    {
        InitializeCharacterComponents();
    }


    private void InitializeCharacterComponents()
    {
        _animator = gameObject.GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    private void OnEnable()
    {
        // Subscribe to character events 
        CharacterMovement.onRun += StartRunAnimation; 
        CharacterMovement.onStop += StopRunAnimation;

        CharacterJump.onJump += StartJumpAnimation;
        CharacterJump.onFalling += StopJumpAnimation;
        CharacterJump.onFalling += StartFallAnimation;
        CharacterJump.onLand += StartLandingAnimation;
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
            _animator.enabled = false;
        }
    }

    private void StopJumpAnimation()
    {
        // If default sprite is available set to the character
        // and run animation system.
        if (_defaultSprite != null)
        {
            _spriteRenderer.sprite = _defaultSprite;
            _animator.enabled = true;
        }
    }


    private void StartFallAnimation()
    {
        //int hash = Animator.StringToHash("Falling");
        //_animator.Play(hash, 0, 0.25f);
        _animator.SetTrigger("Falling");
    }


    private void StartLandingAnimation()
    {
        _animator.SetTrigger("Landing");
    }
}
