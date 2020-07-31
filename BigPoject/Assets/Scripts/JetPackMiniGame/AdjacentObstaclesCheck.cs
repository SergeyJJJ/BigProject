using System;
using UnityEngine;

namespace JetPackMiniGame
{
    public class AdjacentObstaclesCheck : MonoBehaviour
    {
        private bool _isIntersectAnotherObstacle = false;
        
        #region Properties

        public bool IsIntersectAnotherObstacle => _isIntersectAnotherObstacle;

        #endregion Properties


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                _isIntersectAnotherObstacle = true;
                Debug.Log(true);
            }
            else
            {
                _isIntersectAnotherObstacle = false;
            }
        }
    }
}
