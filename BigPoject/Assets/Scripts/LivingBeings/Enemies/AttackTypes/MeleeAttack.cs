using System.Collections;
using UnityEngine;

namespace LivingBeings.Enemies.AttackTypes
{
    // Class that provides functionality to perform melee attack.
    public class MeleeAttack : Attack
    {
        [SerializeField] private float _damagePerAttack = 0;                // Determine how much damage will be applied to the player.
        [SerializeField] private float _timeBeforeDamageApplying = 0;       // Determine how long needed to wait before applying damage to the player.
        
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

                StartCoroutine(ApplyDamageRoutine());
                
                NextAttackTimer = TimeBetweenAttacks;
            }
        }


        private IEnumerator ApplyDamageRoutine()
        {
            yield return new WaitForSeconds(_timeBeforeDamageApplying);
            
            PlayerHealth.TakeDamage(_damagePerAttack);
        }
    }
}