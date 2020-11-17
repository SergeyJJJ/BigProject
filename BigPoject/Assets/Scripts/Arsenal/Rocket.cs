using UnityEngine;

namespace Arsenal
{
    [CreateAssetMenu(menuName = "WeaponProjectile/Rocket", fileName = "Rocket")]
    public class Rocket : WeaponProjectile
    {
        [SerializeField] private float _explosionRadius = 0f;

        #region Properties

        public float ExplosionRadius => _explosionRadius;

        #endregion Properties
    }
}
