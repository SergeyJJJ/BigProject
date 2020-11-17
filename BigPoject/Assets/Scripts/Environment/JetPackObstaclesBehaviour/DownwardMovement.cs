using UnityEngine;

namespace Environment.JetPackObstaclesBehaviour
{
    public class DownwardMovement : MonoBehaviour
    {
        [SerializeField] private float _downwardMovementSpeed = 0f;              // Downward movement speed of an obstacle.
        
        private void FixedUpdate()
        {
            transform.Translate(Vector2.down * (_downwardMovementSpeed * Time.deltaTime), Space.World);
        }
    }
}
