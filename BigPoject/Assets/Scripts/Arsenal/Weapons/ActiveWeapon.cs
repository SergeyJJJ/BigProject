using Destructables.Player;
using UnityEngine;

namespace Arsenal.Weapons
{
    public class ActiveWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon _currentWeapon = null;
        [SerializeField] private CustomButton _fireButton = null;
        
        private void FixedUpdate()
        {
            bool isFireButtonPressed = _fireButton.IsPressed;

            if (_fireButton.IsPressed)
            {
                _currentWeapon.AllowShoot(true);
            }
            else
            {
                _currentWeapon.AllowShoot(false);
            }
        }
    }
}