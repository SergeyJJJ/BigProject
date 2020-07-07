using UnityEngine;

namespace Enemies.AttackTypes
{
    public abstract class Attack : MonoBehaviour
    {
        [SerializeField] private float _attackFrequency = 0;         // How often will the enemy attack;
        private float _nextAttackTimer = 0f;                         // Timer that control when enemy can shoot again.
        private float _timeBetweenAttacks = 0f;                      // Time that must pass between each attack.
        
        #region ForDevelopment

        protected SpriteRenderer _spriteRenderer = null;               // Use this sprite renderer to show attack/
        
        #endregion ForDevelopment
        
        #region Properties

        protected float AttackFrequency => _attackFrequency;

        protected float NextAttackTimer
        {
            get => _nextAttackTimer;
            set => _nextAttackTimer = value;
        }

        protected float TimeBetweenAttacks => _timeBetweenAttacks;

        #endregion Properties
        
        public abstract void AttackPlayer();

        private float GetTimeBetweenEachAttack()
        {
            float secondsPerMinute = 60;
            return secondsPerMinute / _attackFrequency;
        }
        
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _timeBetweenAttacks = GetTimeBetweenEachAttack();
            _nextAttackTimer = _timeBetweenAttacks;
        }
    }
}
