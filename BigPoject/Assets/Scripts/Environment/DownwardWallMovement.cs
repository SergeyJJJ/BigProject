using UnityEngine;

namespace Environment
{
    public class DownwardWallMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 0f;           // Downward movement speed of the walls.
        private BoxCollider2D _wallLenghtCollider;                    // Collider that used for define wall length.
        private Vector2 _startPosition = new Vector2();               // Start position of the walls.
        private float _wallsHalfLength = 0;                           // Half length of the walls.
        private bool _isWallCanMove = false;                          // Check if walls can move now.

        public void StartMovement()
        {
            _isWallCanMove = true;
        }


        public void StopMovement()
        {
            _isWallCanMove = false;
        }


        private void Start()
        {
            StartMovement();
            _wallLenghtCollider = GetComponent<BoxCollider2D>();
            _wallsHalfLength = _wallLenghtCollider.size.y / 2;
        }


        private void FixedUpdate()
        {
            if (_isWallCanMove)
            {
                // Move walls until they are half way.
                // Then returning them to their start position.
                if (IsWallsHalfwayThrough())
                {
                    SetWallsPositionToStart();
                }
            
                Move();
            }
        }


        private void Move()
        {
            transform.Translate(Vector2.down * (_movementSpeed * Time.deltaTime), Space.World);
        }


        private void SetWallsPositionToStart()
        {
            transform.position = _startPosition;
        }


        private bool IsWallsHalfwayThrough()
        {
            // If the distance covered by the wall is more than
            // half length of the wall then function return true
            // and we reset walls position to the start.
            return (Mathf.Abs(transform.position.y - _startPosition.y)) > _wallsHalfLength;
        }
    }
}
