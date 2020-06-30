using System;
using Arsenal.Bullets;
using UnityEngine;

namespace Arsenal.Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Shotgun : BulletWeapon
    {
        [SerializeField] private float _heightBetweenOneShotBullets = 0; // Height between each bullet in one shot.
        private const int _bulletPerShot = 3;                            // Bullets amount that will be launched per one shot.
        
        
        private void Awake()
        {
            WeaponSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }


        private void Start()
        {
            WeaponSpriteRenderer.sprite = InGameSprite;
            TimeBetweenShoots = GetTimeBetweenShoot();
            CurrentBulletCount = BulletsAmount;
        }
        
        
        private void Update()
        {
            if (IsShotTriggered)
            {
                if (IsEnoughBullets())
                {
                    if (IsTimeForShootCome())
                    {
                        Vector2 startLaunchPosition = FirePoint.position;
                        NextShootTimer = TimeBetweenShoots;
                        for (var bulletNumber = 0; bulletNumber < _bulletPerShot; bulletNumber++)
                        {
                            GameObject bullet = GetBullet();
                            Vector2 launchDirection = GetLaunchDirection();
                            InitializeBullet(bullet, launchDirection, startLaunchPosition);
                            LaunchBullet(bullet);
                            DecrementBulletsCount();
                            startLaunchPosition = new Vector2(startLaunchPosition.x, startLaunchPosition.y - _heightBetweenOneShotBullets);
                        }
                    }
                }
            }
            
            NextShootTimer -= Time.deltaTime;
        }
        
        
        protected override void CallShotEvent()
        {
            //
        }
    }
}
