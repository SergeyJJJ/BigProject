using Arsenal.WeaponsProjectiles.ProjectilesData;
using Environment.ThingsDestruction;
using LivingBeings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arsenal.WeaponsProjectiles.ProjectilesBehaviour
{
    public class ActiveBullet : ActiveProjectile
    {
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
                    destruction.Break(CurrentProjectile.Damage);
                }
                else
                {
                    Health health = other.gameObject.GetComponent<Health>();
                    if (health != null)
                    {
                        RaycastHit2D hitRay = new RaycastHit2D();     // Used to determine point in which bullet collide with object.
                        hitRay = Physics2D.Raycast(transform.position, transform.forward, HittableByProjectile);
                        health.TakeDamage(CurrentProjectile.Damage);
                    }
                }

                DisableProjectile();
            }
        }
    } 
}
