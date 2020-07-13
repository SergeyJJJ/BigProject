using UnityEngine;

namespace EntitiesWithHealth.Enemies.AttackTypes
{
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