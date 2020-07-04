using Arsenal.Bullets;
using UnityEngine;

namespace Arsenal.Weapons
{
    public abstract class BulletWeapon : Weapon
    {
        [SerializeField] private Bullet _bulletType = null;         // Contains information about bullet that current weapon will use.
        [SerializeField] private float _fireRate = 0f;              // Weapon fire rate. Shots per minute: from 0.
        private float _nextShootTimer = 0f;                         // Timer that control when player can shoot again.
        private float _timeBetweenShoots = 0f;                      // Time that must pass between each shoot.
    
        #region Properties

        protected Bullet BulletType => _bulletType;

        public float FireRate => _fireRate;

        protected float NextShootTimer
        {
            get => _nextShootTimer;
            set => _nextShootTimer = value > -1 ? value : _nextShootTimer;
        }

        protected float TimeBetweenShoots
        {
            get => _timeBetweenShoots;
            set => _timeBetweenShoots = value;
        }

        #endregion Properties
    
        protected GameObject GetBullet()
        {
            GameObject bullet = null;
            bullet = BulletPool.SharedInstance.GetBullet();
            return bullet;
        }
    
    
        protected void InitializeBullet(GameObject bullet, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            ActiveBullet activeBullet = bullet.GetComponent<ActiveBullet>();
            activeBullet.Initialize(BulletType, launchDirection, startLaunchPosition);
        }
    
    
        protected void LaunchBullet(GameObject bullet)
        {
            bullet.SetActive(true);
        }
    
    
        protected Vector2 GetLaunchDirection()
        {
            return transform.right;
        }
    
    
        protected float GetTimeBetweenShoot()
        {
            float secondsPerMinute = 60;
            return secondsPerMinute / FireRate;
        }
    

        protected bool IsTimeForShootCome()
        {
            return NextShootTimer <= 0f;
        }
    }
}
