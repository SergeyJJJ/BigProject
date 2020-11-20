using ForItemsAndCreatures;
using UnityEngine;

namespace Arsenal.WeaponsProjectiles.ProjectilesBehaviour
{
    public class ActiveRocket : ActiveProjectile
    {
        [SerializeField] private Explosion _explosion = null;   // Component used to create explosion. 
        
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (IsObjectHittableByProjectile(other.gameObject))
            {
                _explosion.Initialize();
            }
        }


        private void GetDamageableObjectsColliders()
        {
            
        }
    }
}
