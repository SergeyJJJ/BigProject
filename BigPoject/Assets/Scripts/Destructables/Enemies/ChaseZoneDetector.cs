using UnityEngine;

namespace Destructables.Enemies
{
    public class ChaseZoneDetector : MonoBehaviour
    {
        private bool _isPlayerInDangerZone = false;

        #region Properties

        public bool IsPlayerInDangerZone=> _isPlayerInDangerZone;

        #endregion Properties

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInDangerZone = true;
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerInDangerZone = false;
            }
        }
    }
}
