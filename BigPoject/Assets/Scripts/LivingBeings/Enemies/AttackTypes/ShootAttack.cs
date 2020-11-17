using Arsenal;
using ForItemsAndCreatures;
using UnityEngine;

namespace LivingBeings.Enemies.AttackTypes
{
    // Class that provides functionality to perform shooting attack.
    public class ShootAttack : Attack
    {
        [SerializeField] private Bullet _bulletType = null;         // Contains information about bullet that current weapon will use.
        [SerializeField] private GameObject _firePoint = null;      // Position in which bullet will appear.
        [SerializeField] private ObjectPool _bulletPool = null;     // Pool from which we get bullets to use.
        
        public override void AttackPlayer()
        {
            if (IsTimeForShootCome())
            {
                NextAttackTimer = TimeBetweenAttacks;

                GameObject bullet = GetBullet();
                Vector2 launchDirection = GetLaunchDirection();
                InitializeBullet(bullet, launchDirection, _firePoint.transform.position);
                LaunchBullet(bullet);
            }

            NextAttackTimer -= Time.deltaTime;
        }
        
        
        private GameObject GetBullet()
        {
            GameObject bullet = null;
            bullet = _bulletPool.GetPooledObject();
            return bullet;
        }
        
        
        private void InitializeBullet(GameObject bullet, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            ActiveBullet activeBullet = bullet.GetComponent<ActiveBullet>();
            activeBullet.Initialize(_bulletType, launchDirection, startLaunchPosition);
        }
        
        
        protected void LaunchBullet(GameObject bullet)
        {
            bullet.SetActive(true);
        }
    
    
        protected Vector2 GetLaunchDirection()
        {
            return transform.right;
        }
        

        protected bool IsTimeForShootCome()
        {
            return NextAttackTimer <= 0f;
        }
    }
}
