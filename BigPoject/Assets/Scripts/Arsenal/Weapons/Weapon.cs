using System;
using Arsenal.Bullets;
using UnityEngine;

namespace Arsenal.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private string _name = "";                 // Name of the weapon.
        [SerializeField] private string _description = "";          // Description of the weapon.
        [SerializeField] private Sprite _inGameSprite = null;       // Sprite that will be used for displaying in character`s hand.
        [SerializeField] private int _maxBulletsAmount = 0;         // Maximum bullets amount: from 0.
        [SerializeField] private GameObject _firePoint = null;      // Position in which bullet will appear.
        private SpriteRenderer _weaponSpriteRenderer = null;        // Sprite of the weapon that will be used in game view.
        private int _currentBulletCount = 0;                        // Bullet capacity now.
        private bool _isShotTriggered = false;                      // Check if player trigger shoot button.
        
        #region Protperties
        
        public string Name => _name;

        public string Description => _description;

        protected Sprite InGameSprite => _inGameSprite;
        
        public int BulletsAmount => _maxBulletsAmount;

        public int CurrentBulletCount
        {
            get => _currentBulletCount;
            set => _currentBulletCount = value >= 0 ? value : _currentBulletCount;
        }
        
        protected GameObject FirePoint => _firePoint;

        public SpriteRenderer WeaponSpriteRenderer
        {
            get => _weaponSpriteRenderer;
            set => _weaponSpriteRenderer = value;
        }

        public bool IsShotTriggered
        {
            get => _isShotTriggered;
            set => _isShotTriggered = value;
        }

        #endregion Properties

        public void AllowShoot(bool canShoot)
        {
            _isShotTriggered = canShoot;
        }

        
        protected bool IsEnoughBullets()
        {
            return CurrentBulletCount > 0;
        }
        
        
        protected void DecrementBulletsCount()
        {
            _currentBulletCount--;
        }

        
        protected virtual void CallShotEvent()
        {
            //
        }
        
        
        protected virtual void CallStopShotEvent()
        {
            //
        }
    }
}
