using System;
using GameBehaviour;
using UnityEngine;

namespace Arsenal.Weapons
{
    public class WeaponsMuzzleFlash : MonoBehaviour
    {
        [SerializeField] private Animator _animator = null;                                                        // Contains animator component that control weapons muzzle flash animations.

        private static readonly int _rifleMuzzleFlash = Animator.StringToHash("AutoRifleMuzzleFlash");      // Hashed name of trigger that allow transition to rifle muzzle flash state.


        private void OnEnable()
        {
            EventSystem.StartListening("OnRifleShot", StartAutoRifleMuzzleFlash);
        }


        private void StartAutoRifleMuzzleFlash()
        {
            _animator.SetTrigger(_rifleMuzzleFlash);
        }


        private void OnDisable()
        {
            EventSystem.StopListening("OnRifleShot", StartAutoRifleMuzzleFlash);
        }
    }
}
