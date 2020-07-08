﻿using UnityEngine;

namespace Creatures.Enemies.AttackTypes
{
    public abstract class Attack : MonoBehaviour
    {
        [SerializeField] private float _damagePerAttack = 0;
        [SerializeField] private float _attackPerMinute = 0;         // How often will the enemy attack;
        private float _nextAttackTimer = 0f;                         // Timer that control when enemy can shoot again.
        private float _timeBetweenAttacks = 0f;                      // Time that must pass between each attack.

        private Health _playerHealth = null;                         // Player health component needed to apply damage to the player.
        
        #region ForDevelopment

        protected SpriteRenderer _spriteRenderer = null;               // Use this sprite renderer to show attack/
        
        #endregion ForDevelopment
        
        #region Properties

        protected float DamagePerAttack => _damagePerAttack;

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
        
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _timeBetweenAttacks = GetTimeBetweenEachAttack();
            _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }
    }
}
