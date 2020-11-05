using System;
using UnityEngine;

namespace Arsenal.Weapons
{
    public class WeaponsSwitcher : MonoBehaviour
    {
        [SerializeField] private Weapon[] _weapons = new Weapon[5];         // Array that stores weapons.
        [SerializeField] private ActiveWeapon _activeWeapon = null;                 // Used to set active weapon at this moment.

        public void SwitchToWeapon(Weapon weapon)
        {
            DeactivateWeaponsOtherThan(weapon);
            _activeWeapon.CurrentWeapon = weapon;
            weapon.gameObject.SetActive(true);
        }
        
        
        private void DeactivateWeaponsOtherThan(Weapon activeWeapon)
        {
            foreach (Weapon weapon in _weapons)
            {
                if (weapon != activeWeapon)
                {
                    weapon.gameObject.SetActive(false);
                }
            }
        }
    }
}
