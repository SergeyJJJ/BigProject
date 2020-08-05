using Arsenal.Bullets;
using UnityEngine;

namespace Arsenal.Weapons
{
    [RequireComponent((typeof(SpriteRenderer)))]
    public class AutoRifle : BulletWeapon
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

                        GameObject bullet = GetBullet();
                        Vector2 launchDirection = GetLaunchDirection();
                        InitializeBullet(bullet, launchDirection, FirePoint.transform.position);
                        LaunchBullet(bullet);
                        CallShotEvent();
                        DecrementBulletsCount();
                    }
                }
            }
            
            NextShootTimer -= Time.deltaTime;
        }
        
        
        protected override void CallShotEvent()
        {
            EventSystem.EventSystem.TriggerEvent("OnRifleShot");
        }
    }
}
