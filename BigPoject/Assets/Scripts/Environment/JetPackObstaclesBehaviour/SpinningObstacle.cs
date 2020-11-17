using UnityEngine;

namespace Environment.JetPackObstaclesBehaviour
{
    public class SpinningObstacle : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 0f;               // Rotating speed of an obstacle.
        
        private void FixedUpdate()
        {
            transform.Rotate(Vector3.forward, _rotateSpeed * Time.deltaTime, Space.Self);
        }
    }
}
