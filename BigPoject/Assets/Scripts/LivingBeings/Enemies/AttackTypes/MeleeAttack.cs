using System;
using System.Collections;
using UnityEngine;

namespace LivingBeings.Enemies.AttackTypes
{
    // Class that provides functionality to perform melee attack.
    public class MeleeAttack : Attack
    {
        [SerializeField] private float _damagePerAttack = 0;                // Determine how much damage will be applied to the player.
        [SerializeField] private float _timeBeforeDamageApplying = 0;       // Determine how long needed to wait before applying damage to the player.
        [SerializeField] private Transform _attackPoint = null;             // Use this point as a point from which attack is performed.
        
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

            Vector2 hurtPosition = GetHurtPosition();
            PlayerHealth.TakeHurt(_damagePerAttack, hurtPosition);
        }
        
        
        private Vector2 GetHurtPosition()
        {
            // Throw raycast from attack point, than
            // get a position at which raycast collides with Player
            // and return it as hurt position.
            float rayDistance = 2f;

            RaycastHit2D hitRay = Physics2D.Raycast(transform.position, transform.forward, rayDistance, 1 << LayerMask.NameToLayer("Player"));
            
            return hitRay.point;
        }
    }
}