using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class CharacterAnimations : MonoBehaviour
    {
        private Animator _animator = null;                  // Contains animator component that control character animations.

        private void Awake()
        {  
            _animator = gameObject.GetComponent<Animator>();
        }


        private void OnEnable()
        {
            // Subscribe to character events 
            CharacterEventSystem.StartListening("OnRun", StartRunAnimation);
            CharacterEventSystem.StartListening("OnStop", StartIdleAnimation);
            CharacterEventSystem.StartListening("OnJump", StartJumpAnimation);
            CharacterEventSystem.StartListening("OnFall", StartFallAnimation);
            CharacterEventSystem.StartListening("OnLand", StartLandingAnimation);
            CharacterEventSystem.StartListening("OnStartClimb", StopAnimations);
            CharacterEventSystem.StartListening("OnStartClimb", StartClimbingAnimation);
            CharacterEventSystem.StartListening("OnStopClimb", StopClimbingAnimation);
        }
        

        private void EnableAnimator()
        {
            _animator.enabled = true;
        }


        private void DisableAnimator()
        {
            _animator.enabled = false;
        }


        private void StopAnimations()
        {
            _animator.SetBool("Run", false);
            _animator.SetBool("Fall", false);
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


        private void StartClimbingAnimation()
        {
            _animator.SetBool("Climb", true);
        }


        private void StopClimbingAnimation()
        {
            _animator.SetBool("Climb", false);
        }
        
        
        private void OnDisable()
        {
            // Unsubscribe to character events
            CharacterEventSystem.StopListening("OnRun", StartRunAnimation);
            CharacterEventSystem.StopListening("OnStop", StartIdleAnimation);
            CharacterEventSystem.StopListening("OnJump", StartJumpAnimation);
            CharacterEventSystem.StopListening("OnFall", StartFallAnimation);
            CharacterEventSystem.StopListening("OnLand", StartLandingAnimation);
            CharacterEventSystem.StopListening("OnClimb", StopAnimations);
            CharacterEventSystem.StopListening("OnClimb", StartClimbingAnimation);
            CharacterEventSystem.StopListening("OnStopClimb", StopClimbingAnimation);
        }
    }
}
