using UnityEngine;

namespace Destructables.Enemies.AttackTypes
{
    public class MeleeAttack : Attack
    {
        public override void AttackPlayer()
        {
            NextAttackTimer -= Time.deltaTime;

            if (NextAttackTimer < 0)
            {
                PlayerHealth.TakeDamage(DamagePerAttack);
                NextAttackTimer = TimeBetweenAttacks;
            }
        }
    }
}