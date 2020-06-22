using UnityEngine;

namespace Assets.Scripts.Arsenal.Weapons
{
    public class ActiveWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon _currentWeapon = null;
        
        public void Shoot()
        {
            _currentWeapon.Shoot();
        }
    }
}