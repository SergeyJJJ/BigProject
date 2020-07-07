using System;
using UnityEngine;
using UnityEngine.Timeline;

namespace Creatures
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth = 0;         // Maximum health amount that have entity.
        private float _currentHealth = 0;                      // Current health amount that have entity.

        #region Properties

        public float MaxHealth => _maxHealth;

        public float CurrentHealth
        {
            private get => _currentHealth;
            set => throw new NotImplementedException(); 
        }
        
        #endregion Properties

        public void TakeDamage(float damageAmount)
        {
            CurrentHealth = CurrentHealth - damageAmount;
            Debug.Log(_currentHealth);
        }
        
        private void Awake()
        {
            _currentHealth = _maxHealth;
        }
    }
}
