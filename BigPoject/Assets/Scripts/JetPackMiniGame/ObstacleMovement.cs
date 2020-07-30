using UnityEngine;

namespace JetPackMiniGame
{
    public class ObstacleMovement : MonoBehaviour
    {
        [SerializeField] private float _downwardSpeed = 0f;                   // Downward movement speed of an obstacle.

        private void FixedUpdate()
        {
            MoveDown();
        }


        private void MoveDown()
        {
            transform.Translate(Vector2.down * (_downwardSpeed * Time.deltaTime), Space.Self);
        }
    }
}
