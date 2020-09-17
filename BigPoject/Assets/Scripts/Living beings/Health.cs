using UnityEngine;

namespace Living_beings
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth = 0;                                // Maximum health amount that have entity.

        [Header("Effects")] [SerializeField] private ParticleSystem _blood = null;    // Particles that used to represent blood. 
        
        private float _currentHealth = 0;                                             // Current health amount that have entity.

        #region Properties

        public float MaxHealth => _maxHealth;

        public float CurrentHealth
        {
            get => _currentHealth;
            
            set
            {
                if (value < 0)
                {
                    _currentHealth = 0;
                }
                else if (value > _maxHealth)
                {
                    _currentHealth = _maxHealth;
                }
                else
                {
                    _currentHealth = value;
                }
            }
        }

        #endregion Properties

        public void TakeDamage(float damageAmount)
        {
            if (damageAmount < 0)
            {
                damageAmount = -damageAmount;
            }
            
            CurrentHealth -= damageAmount;

            if (_blood != null)
            {
                _blood.Play();
            }
        }


        public void TakeTreatment(float hpAmount)
        {
            if (hpAmount < 0)
            {
                hpAmount = -hpAmount;
            }

            CurrentHealth += hpAmount;
        }


        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }
        
        
        private void Awake()
        {
            _currentHealth = _maxHealth;
        }
    }
}
