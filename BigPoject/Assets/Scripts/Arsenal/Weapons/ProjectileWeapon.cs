using Arsenal.WeaponsProjectiles.ProjectilesBehaviour;
using Arsenal.WeaponsProjectiles.ProjectilesData;
using ForItemsAndCreatures;
using UnityEngine;

namespace Arsenal.Weapons
{
    public class ProjectileWeapon : Weapon
    {
        [SerializeField] private float _fireRate = 0f;                    // Weapon fire rate. Shots per minute: from 0.
        [SerializeField] private ObjectPool _projectilePool = null;       // Pool from which we get projectiles to use.
        private float _nextShootTimer = 0f;                               // Timer that control when player can shoot again.
        private float _timeBetweenShoots = 0f;                            // Time that must pass between each shoot.

        #region Properties

        protected float NextShootTimer
        {
            get => _nextShootTimer;
            set => _nextShootTimer = value > -1 ? value : _nextShootTimer;
        }

        protected float TimeBetweenShoots
        {
            get => _timeBetweenShoots;
            set => _timeBetweenShoots = value;
        }

        #endregion Properties

        protected GameObject GetProjectile()
        {
            GameObject projectile = null;
            projectile = _projectilePool.GetPooledObject();
            return projectile;
        }


        protected void InitializeProjectile(GameObject projectile, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            ActiveProjectile activeProjectile = projectile.GetComponent<ActiveProjectile>();
            //activeProjectile.Initialize(_projectile, launchDirection, startLaunchPosition);
        }


        protected void InitializeRocket(GameObject rocketObject, Rocket rocketData, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            ActiveRocket activeRocket = rocketObject.GetComponent<ActiveRocket>();
            activeRocket.InitializeRocket(rocketData, launchDirection, startLaunchPosition);            
        }
        
        
        protected void InitializeBullet(GameObject bulletObject, Bullet bulletData, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            ActiveBullet activeBullet = bulletObject.GetComponent<ActiveBullet>();
            activeBullet.InitializeBullet(bulletData, launchDirection, startLaunchPosition);            
        }


    protected void LaunchProjectile(GameObject projectile)
        {
            projectile.SetActive(true);
        }
    
    
        protected virtual Vector2 GetLaunchDirection()
        {
            return transform.right;
        }
    
    
        protected float GetTimeBetweenShoot()
        {
            const float secondsPerMinute = 60;
            return secondsPerMinute / _fireRate;
        }
    

        protected bool IsTimeForShootCome()
        {
            return NextShootTimer <= 0f;
        }
    }
}
