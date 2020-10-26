using UnityEngine;

namespace Arsenal.Weapons.Lazer
{
    public class LazerAnimationsSynchronizer : MonoBehaviour
    {
        [SerializeField] private Animator _startLazerAnimator = null;           // Animator component of the StartLazer part of the lazer.
        [SerializeField] private Animator _middleLazerAnimator = null;          // Animator component of the MiddleLazer part of the lazer.
        [SerializeField] private Animator _endLazerAnimator = null;             // Animator component of the EndLazer part of the lazer.


        private void OnEnable()
        {
            _startLazerAnimator.Play(0, -1, 0);
            _middleLazerAnimator.Play(0, -1, 0);
            _endLazerAnimator.Play(0, -1, 0);
        }
    }
}
