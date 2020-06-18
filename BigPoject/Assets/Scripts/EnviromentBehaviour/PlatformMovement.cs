using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _trajectoryPoints = null;         // Сontains path points along which the platform moves.
    [SerializeField] private float _movementSpeed = 0f;                        // Contains Platform movement speed.
    private Vector2 _currentTargetPoint = Vector2.zero;                        // Current target point.
    private int _currentPointIndex = 0;    

    private Rigidbody2D _playerRigidbody = null;
    private Vector3 moveDelta = Vector3.zero;

    private void Start()
    {
        if (_trajectoryPoints != null)
        {
            _currentTargetPoint = _trajectoryPoints[0].transform.position;
        }
    }

    private void Update()
    {
        if (IsPointsExist())
        {
            Move();
            if (IsReachedPoint())
            {
                ChangeTargetPoint();
            }
        }
    }


    private void LateUpdate()
    {
        if (_playerRigidbody != null)
        {
            Vector2 playerBody = _playerRigidbody.position;
            _playerRigidbody.transform.position = new Vector3(playerBody.x, playerBody.y) + moveDelta;
        }
    }


    // Move the platform towards the target point.
    private void Move()
    {
        float step = _movementSpeed * Time.deltaTime;
        Vector2 desiredPosition = Vector2.MoveTowards(transform.position, _currentTargetPoint, step);
        Debug.Log(step + " " + desiredPosition);

        moveDelta = new Vector3(desiredPosition.x, desiredPosition.y, 0f) - transform.position;

        transform.position = desiredPosition;
    }


    // Change the position the platform is moving to.
    private void ChangeTargetPoint()
    {
        _currentTargetPoint = GetNextPoint();
    }


    // Method that returns next point from list to be followed.
    private Vector2 GetNextPoint()
    {
        // Here will be stored point to be returned.
        Vector2 nextPoint = Vector2.zero;

        // If current point index is equal to points amount,
        // reset point index to zero and set first point to return.
        if (_currentPointIndex == _trajectoryPoints.Count - 1)
        {
            _currentPointIndex = 0;
            nextPoint = _trajectoryPoints[_currentPointIndex].position;
        }
        // If it is not, than increment point index and set next point to return.
        else
        {
            nextPoint = _trajectoryPoints[_currentPointIndex + 1].position;
            _currentPointIndex++;
        }

        // return index.
        return nextPoint;
    }


    // Check if platform is reached point.
    private bool IsReachedPoint()
    {
        float threshold = 0.1f;

        // If distance between platform and target point is less than allowable threshold
        // than return that the platform reached the goal.
        return Vector2.Distance(gameObject.transform.position, _currentTargetPoint) < threshold;
    }


    private bool IsPointsExist()
    {
        return _trajectoryPoints != null;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
        }
    }
 
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerRigidbody = null;
        }
    }
}
