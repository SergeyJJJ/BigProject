using Arsenal.Bullets;
using UnityEngine;

namespace EntitiesWithHealth.Enemies.AttackTypes
{
    public class ShootAttack : Attack
    {
        [SerializeField] private Bullet _bulletType = null;         // Contains information about bullet that current weapon will use.
        [SerializeField] private GameObject _firePoint = null;      // Position in which bullet will appear.
        
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
            bullet = BulletPool.SharedInstance.GetBullet();
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
