using UnityEngine;

namespace Arsenal.WeaponsProjectiles.ProjectilesData
{
    [CreateAssetMenu(menuName = "WeaponProjectile/Rocket", fileName = "Rocket")]
    public class Rocket : Projectile
    {
        public struct TorqueLimits
        {
            public float minTorque;
            public float maxTorque;
        }
        
        [SerializeField] private float _explosionRadius = 0f;
        [SerializeField] private float _explosionForce = 0;
        [SerializeField] private TorqueLimits _torqueLimits = default(TorqueLimits);

        #region Properties

        public float ExplosionRadius => _explosionRadius;

        public float ExplosionForce => _explosionForce;

        public TorqueLimits ExplosionTorqueLimits => _torqueLimits;

        #endregion Properties
    }
}
