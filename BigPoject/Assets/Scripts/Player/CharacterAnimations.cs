using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class CharacterAnimations : MonoBehaviour
    {
        private Animator _animator = null;                  // Contains animator compnent that controll character animations.

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
        
        
        private void OnDisable()
        {
            // Unsubscribe to character events
            CharacterEventSystem.StopListening("OnRun", StartRunAnimation);
            CharacterEventSystem.StopListening("OnStop", StartIdleAnimation);
            CharacterEventSystem.StopListening("OnJump", StartJumpAnimation);
            CharacterEventSystem.StopListening("OnFall", StartFallAnimation);
            CharacterEventSystem.StopListening("OnLand", StartLandingAnimation);
        }
    }
}
