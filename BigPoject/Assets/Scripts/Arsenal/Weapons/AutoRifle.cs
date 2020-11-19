using GameBehaviour;
using UnityEngine;

namespace Arsenal.Weapons
{
    [RequireComponent((typeof(SpriteRenderer)))]
    public class AutoRifle : ProjectileWeapon
    {
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
                        NextShootTimer = TimeBetweenShoots;

                        GameObject bullet = GetProjectile();
                        Vector2 launchDirection = GetLaunchDirection();
                        InitializeProjectile(bullet, launchDirection, FirePoint.transform.position);
                        RotateBullet(bullet);
                        LaunchProjectile(bullet);
                        CallShotEvent();
                        DecrementBulletsCount();
                    }
                }
            }
            
            NextShootTimer -= Time.deltaTime;
        }


        private void RotateBullet(GameObject bullet)
        {
            bullet.transform.rotation = Quaternion.identity;
        }
        
        
        protected override void CallShotEvent()
        {
            EventSystem.TriggerEvent("OnRifleShot");
        }
    }
}
