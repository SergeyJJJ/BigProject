using UnityEngine;

namespace Assets.Scripts.Arsenal.Bullets
{
    [CreateAssetMenu(menuName = "Arsenal/Bullet", fileName = "Bullet")]
    public abstract class Bullet : ScriptableObject
    {
        [SerializeField] private float _damage = 0f;
        [SerializeField] private float _flightRange = 0f;
        [SerializeField] private float _flightSpeed = 0f;
        [SerializeField] private Sprite _flightSprite = null;

        #region Properties

        public float Damage => _damage;

        public float FlightRange => _flightRange;

        public float FlightSpeed => _flightSpeed;

        public Sprite FlightSprite => _flightSprite;

        #endregion
    }
}
