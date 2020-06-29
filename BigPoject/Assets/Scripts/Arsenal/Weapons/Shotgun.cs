﻿using System;
using Arsenal.Bullets;
using UnityEngine;

namespace Arsenal.Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Shotgun : Weapon
    {
        [SerializeField] private float _heightBetweenOneShotBullets = 0; // Height between each bullet in one shot.
        private SpriteRenderer _weaponSpriteRenderer = null;                   // Sprite of the weapon that will be used in game view.
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
            _weaponSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }


        private void Start()
        {
            _weaponSpriteRenderer.sprite = InGameSprite;
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
                        Vector2 startLaunchPosition = FirePoint.position;
                        NextShootTimer = _timeBetweenShoots;
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
        

        private GameObject GetBullet()
        {
            GameObject bullet = null;
            bullet = BulletPool.SharedInstance.GetBullet();
            return bullet;
        }
        

        private void InitializeBullet(GameObject bullet, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            ActiveBullet activeBullet = bullet.GetComponent<ActiveBullet>();
            activeBullet.Initialize(BulletType, launchDirection, startLaunchPosition);
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
