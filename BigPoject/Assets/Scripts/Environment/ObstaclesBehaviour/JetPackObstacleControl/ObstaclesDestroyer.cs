using UnityEngine;

namespace Environment.ObstaclesBehaviour.JetPackObstacleControl
{
    public class ObstaclesDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Obstacle"))
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
