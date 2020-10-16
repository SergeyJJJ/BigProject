using UnityEngine;

namespace LivingBeings.Enemies.AttackTypes
{
    // Base class for enemy`s attack behaviour.
    public abstract class Attack : MonoBehaviour
    {
        [SerializeField] private float _attackPerMinute = 0;              // How often will the enemy attack;

        private LayerMask _playerLayer = Physics2D.AllLayers;             // Used for raycast to find point where enemy hit the player.
        private Animator _animator = null;                                // Animator that used to control enemy`s animations.
        private bool _isAlreadyAttacking = false;                         // Determine if enemy is attacking right now
        private float _nextAttackTimer = 0f;                              // Timer that control when enemy can shoot again.
        private float _timeBetweenAttacks = 0f;                           // Time that must pass between each attack.
        private Health _playerHealth = null;                              // Player health component needed to apply damage to the player.

        #region Properties

        protected LayerMask PlayerLayer => _playerLayer;

        protected Animator EnemyAnimator => _animator;

        protected bool IsAlreadyAttacking
        {
            get => _isAlreadyAttacking;
            set => _isAlreadyAttacking = value;
        }

        protected float NextAttackTimer
        {
            get => _nextAttackTimer;
            set => _nextAttackTimer = value;
        }

        protected float TimeBetweenAttacks => _timeBetweenAttacks;

        protected Health PlayerHealth => _playerHealth;

        #endregion Properties
        
        public abstract void AttackPlayer();

        public void SetTimeBeforeAttackEqualZero()
        {
            _nextAttackTimer = 0;
        }
        

        private float GetTimeBetweenEachAttack()
        {
            float secondsPerMinute = 60;
            return secondsPerMinute / _attackPerMinute;
        }

        
        protected virtual void Awake()
        {
            _playerLayer = LayerMask.NameToLayer("Player");
            _animator = GetComponent<Animator>();
        }
        
        
        private void Start()
        {
            _timeBetweenAttacks = GetTimeBetweenEachAttack();
            _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }
    }
}
