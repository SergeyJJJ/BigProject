using System;
using UnityEngine;

namespace Enemies
{
    public class PlayerDetector : MonoBehaviour
    {
        private bool _isPlayerDetected = false;

        public bool IsPlayerDetected => _isPlayerDetected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerDetected = true;
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerDetected = false;
            }
        }
    }
}
