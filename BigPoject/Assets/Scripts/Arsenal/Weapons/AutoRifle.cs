using Arsenal.Bullets;
using UnityEngine;

namespace Arsenal.Weapons
{
    [RequireComponent((typeof(SpriteRenderer)))]
    public class AutoRifle : Weapon
    {
        private SpriteRenderer _spriteRenderer = null;               // Sprite of the weapon that will be used in game view.
        private float _nextShootTimer = 0f;                          // Timer that control when player can shoot again.
        private float _timeBetweenShoots = 0f;                       // Time that must pass between each shoot.
        private bool _isShotTriggered = false;                       // Check if player trigger shoot button.

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

                        GameObject bullet = GetBullet();
                        Vector2 launchDirection = GetLaunchDirection();
                        InitializeBullet(bullet, launchDirection);
                        LaunchBullet(bullet);
                        DecrementBulletsCount();
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
            activeBullet.Initialize(BulletType, launchDirection, FirePoint.position);
        }


        private void LaunchBullet(GameObject bullet)
        {
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
