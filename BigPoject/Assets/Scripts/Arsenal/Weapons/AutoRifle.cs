using System.Runtime.InteropServices.WindowsRuntime;
using Assets.Scripts.Arsenal.Bullets;
using UnityEngine;

namespace Assets.Scripts.Arsenal.Weapons
{
    [RequireComponent((typeof(SpriteRenderer)))]
    public class AutoRifle : Weapon
    {
        private SpriteRenderer _spriteRenderer = null;
        private float _nextShootTimer = 0f;
        private float _timeBetweenShoots = 0f;
        private bool _isShotTriggered = false;

        #region Properties

        private float NextShootTimer
        {
            get => _nextShootTimer;
            set => _nextShootTimer = value > -1 ? value : -1;
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
            Debug.Log(_timeBetweenShoots);
        }


        private void Update()
        {
            if (_isShotTriggered)
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


        private void LaunchBullet(GameObject bullet)
        {
            bullet.transform.position = FirePoint.position;
            bullet.SetActive(true);
        }


        private Vector2 GetLaunchDirection()
        {
            return Vector2.right;
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
    }
}
