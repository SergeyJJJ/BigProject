using UnityEngine;

namespace Arsenal.WeaponsProjectiles.ProjectilesData
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
