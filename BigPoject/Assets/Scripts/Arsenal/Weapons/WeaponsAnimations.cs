using System;
using Player;
using UnityEngine;

namespace Arsenal.Weapons
{
    public class WeaponsAnimations : MonoBehaviour
    {
        private Animator _animator = null;                                               // Contains animator component that control weapon animations.
        private static readonly int Recoil = Animator.StringToHash("Recoil");      // Hashed name of trigger that allow transition to recoil state. 

        private void Awake()
        {  
            _animator = gameObject.GetComponent<Animator>();
        }


        private void OnEnable()
        {
            EventSystem.StartListening("OnRifleShot", StartRifleRecoilAnimation);
        }


        private void StartRifleRecoilAnimation()
        {
            _animator.SetTrigger(Recoil);
        }


        private void OnDisable()
        {
            EventSystem.StopListening("OnRifleShot", StartRifleRecoilAnimation);
        }
    }
}
