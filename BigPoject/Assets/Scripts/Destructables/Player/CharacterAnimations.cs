using GameBahaviour;
using UnityEngine;

namespace Destructables.Player
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
            EventSystem.StartListening("OnRun", StartRunAnimation);
            EventSystem.StartListening("OnStop", StartIdleAnimation);
            EventSystem.StartListening("OnJump", StartJumpAnimation);
            EventSystem.StartListening("OnFall", StartFallAnimation);
            EventSystem.StartListening("OnLand", StartLandingAnimation);
            EventSystem.StartListening("OnStartClimb", StopAnimations);
            EventSystem.StartListening("OnStartClimb", StartClimbingAnimation);
            EventSystem.StartListening("OnStopClimb", StopClimbingAnimation);
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
            EventSystem.StopListening("OnRun", StartRunAnimation);
            EventSystem.StopListening("OnStop", StartIdleAnimation);
            EventSystem.StopListening("OnJump", StartJumpAnimation);
            EventSystem.StopListening("OnFall", StartFallAnimation);
            EventSystem.StopListening("OnLand", StartLandingAnimation);
            EventSystem.StopListening("OnClimb", StopAnimations);
            EventSystem.StopListening("OnClimb", StartClimbingAnimation);
            EventSystem.StopListening("OnStopClimb", StopClimbingAnimation);
        }
    }
}
