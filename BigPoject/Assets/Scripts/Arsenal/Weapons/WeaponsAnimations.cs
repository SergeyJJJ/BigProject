using System;
using Player;
using UnityEngine;

namespace Arsenal.Weapons
{
    public class WeaponsAnimations : MonoBehaviour
    {
        private Animator _animator = null;                                                          // Contains animator component that control weapon animations.
        private static readonly int _rifleRecoil = Animator.StringToHash("RifleRecoil");      // Hashed name of trigger that allow transition to rifle recoil state.
        private static readonly int _lazerRecoil = Animator.StringToHash("LazerRecoil");      // Hashed name of trigger that allow transition to lazer recoil state.

        private void Awake()
        {  
            _animator = gameObject.GetComponent<Animator>();
        }


        private void OnEnable()
        {
            EventSystem.StartListening("OnRifleShot", StartRifleRecoilAnimation);
            EventSystem.StartListening("OnLazerShot", StartLazerRecoilAnimation);
            EventSystem.StartListening("OnStopLazerShop", StopLazerRecoilAnimation);
        }


        private void StartRifleRecoilAnimation()
        {
            _animator.SetTrigger(_rifleRecoil);
        }


        private void StartLazerRecoilAnimation()
        {
            _animator.SetBool(_lazerRecoil, true);
        }


        private void StopLazerRecoilAnimation()
        {
            _animator.SetBool(_lazerRecoil, false);
        }


        private void OnDisable()
        {
            EventSystem.StopListening("OnRifleShot", StartRifleRecoilAnimation);
            EventSystem.StopListening("OnLazerShot", StartLazerRecoilAnimation);
            EventSystem.StopListening("OnStopLazerShop", StopLazerRecoilAnimation);
        }
    }
}
