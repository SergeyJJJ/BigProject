using UnityEngine;

namespace Living_beings.Enemies.AttackTypes
{
    // Class that provides functionality to perform melee attack.
    public class MeleeAttack : Attack
    {
        [SerializeField] private float _damagePerAttack = 0;
        
        public override void AttackPlayer()
        {
            NextAttackTimer -= Time.deltaTime;

            if (NextAttackTimer < 0)
            {
                PlayerHealth.TakeDamage(_damagePerAttack);
                NextAttackTimer = TimeBetweenAttacks;
            }
        }
    }
}