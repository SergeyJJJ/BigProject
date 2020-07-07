using UnityEngine;

namespace Enemies
{
    public class AttackZoneDetector : MonoBehaviour
    {
        private bool _isPlayerInAttackZone = false;

        #region Properies

        public bool IsPlayerInAttackZone=> _isPlayerInAttackZone;

        #endregion Properties

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInAttackZone = true;
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInAttackZone = false;
            }
        }
    }
}
