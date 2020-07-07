using UnityEngine;

namespace Creatures
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth = 0; // Maximum health amount that have entity.
        private float _currentHealth = 0; // Current health amount that have entity.

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
