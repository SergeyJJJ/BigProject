using Arsenal.WeaponsProjectiles.ProjectilesData;
using ForItemsAndCreatures;
using UnityEngine;

namespace Arsenal.WeaponsProjectiles.ProjectilesBehaviour
{
    public class ActiveRocket : ActiveProjectile
    {
        [SerializeField] private Explosion _explosion = null;   // Component used to create explosion. 
        private Rocket _currentRocket = null;                   // Used to get data about current specific rocket.
        
        
        public override void InitializeRocket(Rocket rocket, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            _currentRocket = rocket;
            Initialize(rocket, launchDirection, startLaunchPosition);
        }


        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (IsObjectHittableByProjectile(other.gameObject))
            {
                //_explosion.Initialize();
            }
        }


        private void GetDamageableObjectsColliders()
        {
            
        }
    }
}
