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

            IsAlreadyAttacking = false;
            
            if (NextAttackTimer < 0)
            {
                if (!IsAlreadyAttacking)
                {
                    EnemyAnimator.SetTrigger("Attack");
                    EnemyAnimator.SetBool("Idle", true);
                    EnemyAnimator.SetBool("Chase", false);
                    EnemyAnimator.SetBool("Patrol", false);

                    IsAlreadyAttacking = true;
                }
                
                PlayerHealth.TakeDamage(_damagePerAttack);
                NextAttackTimer = TimeBetweenAttacks;
            }
        }
    }
}