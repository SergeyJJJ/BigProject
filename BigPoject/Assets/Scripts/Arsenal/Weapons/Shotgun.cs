using System;
using Arsenal.Bullets;
using UnityEngine;

namespace Arsenal.Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Shotgun : Weapon
    {
        [SerializeField] private float _heightBetweenOneShotBullets = 0; // Height between each bullet in one shot.
        private SpriteRenderer _spriteRenderer = null;                   // Sprite of the weapon that will be used in game view.
        private float _nextShootTimer = 0f;                              // Timer that control when player can shoot again.
        private float _timeBetweenShoots = 0f;                           // Time that must pass between each shoot.
        private bool _isShotTriggered = false;                           // Check if player trigger shoot button.
        private const int _bulletPerShot = 3;                            // Bullets amount that will be launched per one shot.
        
        #region Properties

        private float NextShootTimer
        {
            get => _nextShootTimer;
            set => _nextShootTimer = value > -1 ? value : _nextShootTimer;
        }

        #endregion
        
        public override void AllowShoot(bool canShoot)
        {
            _isShotTriggered = canShoot;
        }
        
        
        private void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }


        private void Start()
        {
            _spriteRenderer.sprite = InGameSprite;
            _timeBetweenShoots = GetTimeBetweenShoot();
            CurrentBulletCount = BulletsAmount;
        }
        
        
        private void Update()
        {
            if (_isShotTriggered)
            {
                if (IsEnoughBullets())
                {
                    if (IsTimeForShootCome())
                    {
                        NextShootTimer = _timeBetweenShoots;
                        for (var bulletNumber = 0; bulletNumber < _bulletPerShot; bulletNumber++)
                        {
                            GameObject bullet = GetBullet();
                            Vector2 launchDirection = GetLaunchDirection();
                            InitializeBullet(bullet, launchDirection);
                            LaunchBullet(bullet, bulletNumber);
                            DecrementBulletsCount();
                        }
                    }
                }
            }
            
            NextShootTimer -= Time.deltaTime;
        }
        

        private GameObject GetBullet()
        {
            GameObject bullet = null;
            bullet = BulletPool.SharedInstance.GetBullet();
            return bullet;
        }
        

        private void InitializeBullet(GameObject bullet, Vector2 launchDirection)
        {
            ActiveBullet activeBullet = bullet.GetComponent<ActiveBullet>();
            activeBullet.Initialize(BulletType, launchDirection);
        }


        private void LaunchBullet(GameObject bullet, int bulletNumber)
        {
            Vector2 firePoint = FirePoint.position;
            
            switch (bulletNumber)
            {
                case 0:
                    bullet.transform.position = new Vector2(firePoint.x, firePoint.y + _heightBetweenOneShotBullets);
                    break;
                case 1:
                    bullet.transform.position = firePoint;
                    break;
                case 2:
                    bullet.transform.position = new Vector2(firePoint.x, firePoint.y - _heightBetweenOneShotBullets);
                    break;
            }
            
            bullet.SetActive(true);
        }


        private Vector2 GetLaunchDirection()
        {
            return transform.right;
        }


        private float GetTimeBetweenShoot()
        {
            float secondsPerMinute = 60;
            return secondsPerMinute / FireRate;
        }


        private bool IsTimeForShootCome()
        {
            return NextShootTimer <= 0f;
        }


        private bool IsEnoughBullets()
        {
            return CurrentBulletCount > 0;
        }
    }
}
