using Arsenal.WeaponsProjectiles.ProjectilesData;
using GameBehaviour;
using UnityEngine;

namespace Arsenal.Weapons
{
    [RequireComponent((typeof(SpriteRenderer)))]
    public class RocketLauncher : ProjectileWeapon
    {
        [SerializeField] private Rocket _rocketData = null;            // Contains data about bullet that used by RocketLauncher.
        
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

                        GameObject rocketObject = GetProjectile();
                        Vector2 launchDirection = GetLaunchDirection();
                        InitializeRocket(rocketObject, _rocketData, launchDirection, FirePoint.transform.position);
                        RotateRocket(rocketObject);
                        LaunchProjectile(rocketObject);
                        //CallShotEvent();
                        DecrementBulletsCount();
                    }
                }
            }
            
            NextShootTimer -= Time.deltaTime;
        }


        private void RotateRocket(GameObject rocket)
        {
            SetProjectileRotationToIdentity(rocket);
            
            if (transform.right.x < 0)
            {
                rocket.transform.Rotate(0, 180, 0);
            }
        }
        
        
        protected override void CallShotEvent()
        {
            //EventSystem.TriggerEvent("OnRifleShot");
        }
    }
}
