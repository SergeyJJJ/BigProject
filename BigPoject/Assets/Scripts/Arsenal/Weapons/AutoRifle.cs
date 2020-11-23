using Arsenal.WeaponsProjectiles.ProjectilesData;
using GameBehaviour;
using UnityEngine;

namespace Arsenal.Weapons
{
    [RequireComponent((typeof(SpriteRenderer)))]
    public class AutoRifle : ProjectileWeapon
    {
        [SerializeField] private Bullet _bulletData = null;            // Contains data about bullet that used by AutoRifle.
    
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

                        GameObject bulletObject = GetProjectile();
                        Vector2 launchDirection = GetLaunchDirection();
                        InitializeBullet(bulletObject, _bulletData, launchDirection, FirePoint.transform.position);
                        SetProjectileRotationToIdentity(bulletObject);
                        LaunchProjectile(bulletObject);
                        CallShotEvent();
                        DecrementBulletsCount();
                    }
                }
            }
            
            NextShootTimer -= Time.deltaTime;
        }
        

        protected override void CallShotEvent()
        {
            EventSystem.TriggerEvent("OnRifleShot");
        }
    }
}
