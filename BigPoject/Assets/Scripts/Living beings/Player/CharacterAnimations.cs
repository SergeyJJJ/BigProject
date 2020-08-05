using UnityEngine;

namespace Living_beings.Player
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
            EventSystem.EventSystem.StartListening("OnRun", StartRunAnimation);
            EventSystem.EventSystem.StartListening("OnStop", StartIdleAnimation);
            EventSystem.EventSystem.StartListening("OnJump", StartJumpAnimation);
            EventSystem.EventSystem.StartListening("OnFall", StartFallAnimation);
            EventSystem.EventSystem.StartListening("OnLand", StartLandingAnimation);
            EventSystem.EventSystem.StartListening("OnStartClimb", StopAnimations);
            EventSystem.EventSystem.StartListening("OnStartClimb", StartClimbingAnimation);
            EventSystem.EventSystem.StartListening("OnStopClimb", StopClimbingAnimation);
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
            EventSystem.EventSystem.StopListening("OnRun", StartRunAnimation);
            EventSystem.EventSystem.StopListening("OnStop", StartIdleAnimation);
            EventSystem.EventSystem.StopListening("OnJump", StartJumpAnimation);
            EventSystem.EventSystem.StopListening("OnFall", StartFallAnimation);
            EventSystem.EventSystem.StopListening("OnLand", StartLandingAnimation);
            EventSystem.EventSystem.StopListening("OnClimb", StopAnimations);
            EventSystem.EventSystem.StopListening("OnClimb", StartClimbingAnimation);
            EventSystem.EventSystem.StopListening("OnStopClimb", StopClimbingAnimation);
        }
    }
}
