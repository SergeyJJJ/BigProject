using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        InitializeaAnimatorComponents();
    }


    private void InitializeaAnimatorComponents()
    {
        _animator = gameObject.GetComponent<Animator>();
    }


    private void OnEnable()
    {
        // Subscribe to character events 
        CharacterMovement.onRun += StartRunAnimation; 
        CharacterMovement.onStop += StopRunAnimation;
    }


    private void StartRunAnimation()
    {
        _animator.SetBool("Run", true);
    }


    private void StopRunAnimation()
    {
        _animator.SetBool("Run", false);
    }
}
