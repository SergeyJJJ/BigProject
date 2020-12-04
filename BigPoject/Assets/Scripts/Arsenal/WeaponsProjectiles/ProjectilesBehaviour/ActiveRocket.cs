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
                ExplodeRocket();
                DisableProjectile();
            }
        }


        private void Update()
        {
            float flownDistance = Mathf.Abs(transform.position.x - StartFlightPosition.x);

            if (flownDistance > FlightRange)
            {
                ExplodeRocket();
                DisableProjectile();
            }
        }


        private void ExplodeRocket()
        {
            _explosion.Initialize(_currentRocket);
            _explosion.Explode();
        }
    }
}
