using UI;
using UnityEngine;

namespace Arsenal.Weapons
{
    public class ActiveWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon _currentWeapon = null;
        [SerializeField] private CustomButton _fireButton = null;

        #region Properties

        public Weapon CurrentWeapon
        {
            get => _currentWeapon;
            set => _currentWeapon = value;
        }

        #endregion
        
        private void FixedUpdate()
        {
            if (_fireButton != null)
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
}