using GameBehaviour;
using UnityEngine;

namespace Arsenal.Weapons.Lazer
{
    public class LazerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _lazerStartAnimator = null;                // Animator component of the lazer start part.
        [SerializeField] private Animator _lazerMiddleAnimator = null;               // Animator component of the lazer middle part.
        [SerializeField] private Animator _lazerEndAnimator = null;                  // Animator component of the lazer end part.

        private void OnEnable()
        {
            EventSystem.StartListening("OnLazerPartActive", SynchronizeLazerParts);
        }
    
    
        private void SynchronizeLazerParts()
        {
            if (_lazerStartAnimator.gameObject.activeInHierarchy)
            {
                _lazerStartAnimator.Play(0, -1, 0);
            }

            if (_lazerMiddleAnimator.gameObject.activeInHierarchy)
            {
                _lazerMiddleAnimator.Play(0, -1, 0);
            }

            if (_lazerEndAnimator.gameObject.activeInHierarchy)
            {
                _lazerEndAnimator.Play(0, -1, 0);
            }
        }


        private void OnDisable()
        {
            EventSystem.StopListening("OnLazerPartActive", SynchronizeLazerParts);
        }
    }
}
