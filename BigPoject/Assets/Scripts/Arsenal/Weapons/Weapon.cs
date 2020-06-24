using Arsenal.Bullets;
using UnityEngine;

namespace Arsenal.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private string _name = "";                 // Name of the weapon.
        [SerializeField] private string _description = "";          // Description of the weapon.
        [SerializeField] private Sprite _inGameSprite = null;       // Sprite that will be used for displaying in character`s hand.
        [SerializeField] private Bullet _bulletType = null;         // Contains information about bullet that current weapon will use.
        [SerializeField] private int _maxBulletsAmount = 0;         // Maximum bullets amount: from 0.
        [SerializeField] private float _fireRate = 0f;              // Weapon fire rate. Shots per minute: from 0. 
        [SerializeField] private Transform _firePoint = null;       // Position in which bullet will appear.
        private int _currentBulletCount = 0;                        // Bullet capacity now.
        
        #region Protperties
        
        public string Name => _name;

        public string Description => _description;

        protected Sprite InGameSprite => _inGameSprite;

        protected Bullet BulletType => _bulletType;

        public int BulletsAmount => _maxBulletsAmount;

        public float FireRate => _fireRate;

        public int CurrentBulletCount
        {
            get => _currentBulletCount;
            set => _currentBulletCount = value >= 0 ? value : _currentBulletCount;
        }
        
        protected Transform FirePoint => _firePoint;

        #endregion Properties
        
        public abstract void AllowShoot(bool canShoot);

        protected void DecrementBulletsCount()
        {
            _currentBulletCount--;
        }
    }
}
