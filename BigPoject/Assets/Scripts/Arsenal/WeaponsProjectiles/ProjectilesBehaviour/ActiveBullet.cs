using Arsenal.WeaponsProjectiles.ProjectilesData;
using Environment.ThingsDestruction;
using LivingBeings;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arsenal.WeaponsProjectiles.ProjectilesBehaviour
{
    public class ActiveBullet : ActiveProjectile
    {
        private Bullet _currentBullet = null;             // Used to get data about current specific bullet.
        
        public override void InitializeBullet(Bullet bullet, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            _currentBullet = bullet;
            Initialize(bullet, launchDirection, startLaunchPosition);
        }
        
        
        public void RandomizeLifeTimeInScatter(float scatter)
        {
            FlightRange += Random.Range(-scatter, scatter);
        }
        
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (IsObjectHittableByProjectile(other.gameObject))
            {
                Destruction destruction = other.gameObject.GetComponent<Destruction>();
                if (destruction != null)
                {
                    destruction.Break(_currentBullet.Damage);
                }
                else
                {
                    Health health = other.gameObject.GetComponent<Health>();
                    if (health != null)
                    {
                        RaycastHit2D hitRay = new RaycastHit2D();     // Used to determine point in which bullet collide with object.
                        hitRay = Physics2D.Raycast(transform.position, transform.forward, HittableByProjectile);
                        health.TakeDamage(_currentBullet.Damage);
                    }
                }

                DisableProjectile();
            }
        }
    } 
}
