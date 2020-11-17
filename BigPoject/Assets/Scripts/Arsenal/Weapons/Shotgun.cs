using System;
using GameBehaviour;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arsenal.Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Shotgun : BulletWeapon
    {
        [Serializable]
        public struct BulletDirectionBoundaries
        {
            public float yAxisMin;
            public float yAxisMax;
            public float xAxisMin;
            public float xAxisMax;
        }

        [SerializeField] private BulletDirectionBoundaries _bulletDirectionBoundaries;       // Used to define min and max bullet launch direction coordinates.
        [SerializeField] private int _bulletsPerShot = 6;                                    // Bullets amount that will be launched per one shot.

        private int _currentShotBulletIndex = 0;                                             // Used to get appropriate bullets direction. 
        private float _heightBetweenBullets = 0;                                             // Define distance between bullets on y-Axis.  
        
        private void Awake()
        {
            WeaponSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        
        
        private void Start()
        {
            WeaponSpriteRenderer.sprite = InGameSprite;
            TimeBetweenShoots = GetTimeBetweenShoot();
            CurrentBulletCount = BulletsAmount;
            _heightBetweenBullets = Mathf.Abs((_bulletDirectionBoundaries.yAxisMax - _bulletDirectionBoundaries.yAxisMin)) / _bulletsPerShot;
        }


        private void Update()
        {
            if (IsShotTriggered)
            {
                if (IsEnoughBullets())
                {
                    if (IsTimeForShootCome())
                    {
                        float lifeTimeScatter = 0.3f;
                        Vector2 startLaunchPosition = FirePoint.transform.position;
                        NextShootTimer = TimeBetweenShoots;
                        
                        while (_currentShotBulletIndex < _bulletsPerShot)
                        {
                            GameObject bullet = GetBullet();
                            Vector2 launchDirection = GetLaunchDirection();
                            InitializeBullet(bullet, launchDirection.normalized, startLaunchPosition);
                            RotateBulletToDirection(bullet, launchDirection);
                            ActiveBullet activeBullet = bullet.GetComponent<ActiveBullet>();
                            activeBullet.RandomizeLifeTimeInScatter(lifeTimeScatter);
                            LaunchBullet(bullet);
                            CallShotEvent();
                            
                            _currentShotBulletIndex++;
                        }

                        DecrementBulletsCount();
                        _currentShotBulletIndex = 0;
                    }
                }
            }
            NextShootTimer -= Time.deltaTime;
        }
        
        
        protected override Vector2 GetLaunchDirection()
        {
            float xDirection = _bulletDirectionBoundaries.xAxisMax * transform.right.x;
            float yDirection = _bulletDirectionBoundaries.yAxisMin + (_heightBetweenBullets * _currentShotBulletIndex);
            float backlash = 0.5f;
            float yDirectionWithBacklash = yDirection + Random.Range(-backlash, backlash);

            return new Vector2(xDirection, yDirectionWithBacklash);
        }


        private void RotateBulletToDirection(GameObject bullet, Vector2 targetDirection)
        {
            Vector2 bulletLaunchPosition = FirePoint.transform.localPosition;
            Vector2 lookDirection = targetDirection - bulletLaunchPosition;
            float rotationAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            
            bullet.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
        
        
        protected override void CallShotEvent()
        {
            EventSystem.TriggerEvent("OnShotgunShot");
        }
    }
}