using UnityEngine;

namespace Enemies.AttackTypes
{
    public class MeleeAttack : Attack
    {
        public override void AttackPlayer()
        {
            NextAttackTimer -= Time.deltaTime;

            if (NextAttackTimer < 0)
            {
                // Attack code
                Debug.Log("Attack");
                NextAttackTimer = TimeBetweenAttacks;
            }
        }
    }
}