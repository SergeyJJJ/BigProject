using Assets.Scripts.Arsenal.Bullets;
using UnityEngine;

namespace Assets.Scripts.Arsenal.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private string _name = "";                 // Name of the weapon.
        [SerializeField] private string _description = "";          // Description of the weapon.
        [SerializeField] private Sprite _inGameSprite = null;       // Sprite that will be used for displaying in character`s hand.
        [SerializeField] private Bullet _bulletType = null;         // Contains information about bullet that current weapon will use.
        [SerializeField] private int _maxBulletsAmount = 0;         // Maximum bullets amount.
        [SerializeField] private float _fireRate = 0f;              // Weapon fire rate. 
        [SerializeField] private Transform _firePoint = null;              // Position in which bullet will appear.
        private int _currentBulletCount = 0;                        // Bullet capacity now.
        
        #region Protperties
        
        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        protected Sprite InGameSprite
        {
            get { return _inGameSprite; }
        }

        protected Bullet BulletType
        {
            get { return _bulletType; }
        }

        public int BulletsAmount
        {
            get { return _maxBulletsAmount; }
        }

        public float FireRate
        {
            get { return _fireRate; }
        }

        public int CurrentBulletCount
        {
            get { return _currentBulletCount; }
            set { _currentBulletCount = value; }
        }
        
        protected Transform FirePoint => _firePoint;

        #endregion Properties
        
        public abstract void Shoot();

        protected void DecrementBulletsCount()
        {
            _currentBulletCount--;
        }
    }
}
