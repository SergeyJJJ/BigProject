using System;
using UnityEngine;
using Pathfinding;

namespace EntitiesWithHealth.Enemies.ChaseTypes
{
    // Class that provides functionality to perform flying chase.
    public class FlyingChase : Chase
    {
        [SerializeField] private float _nextWaypointDistance = 0f;  // Distance between enemy and waypoint to be reached to go to the next waypoint.
        private Seeker _seeker = null;                             // Seeker component that allows us to use A* seeker functionality.
        private Path _path = null;                                 // Path to the player.
        private int _currentWaypoint = 0;                          // Store current waypoint along path that we are targeting.
        //private bool _isEndOfPathReached = false;                  // Check if end of path is reached.
        
        public override void ChasePlayer(Transform enemyTransform, Transform playerTransform, Rigidbody2D enemyRigidbody)
        {
            _seeker.StartPath(enemyTransform.position, playerTransform.position, OnPathComplete);

            if (IsPathExists() && IsThereWaypointsToFollow())
            {
                Vector2 chaseDirection = (_path.vectorPath[_currentWaypoint] - enemyTransform.position).normalized;
                enemyRigidbody.velocity = chaseDirection * (ChasingSpeed * Time.deltaTime);

                if (IsWaypointReached(enemyTransform))
                {
                    _currentWaypoint++;
                }
            
                // Change facing direction if needed.
                if (IsPlayerToTheRight(playerTransform) && !IsFacingRight)
                {
                    Flip();
                }
                else if (IsPlayerToTheLeft(playerTransform) && IsFacingRight)
                {
                    Flip();
                }
            } 
        }


        private void OnPathComplete(Path path)
        {
            if (!path.error)
            {
                _path = path;
                _currentWaypoint = 0;
            }
        }


        private bool IsPathExists()
        {
            return _path != null;
        }


        private bool IsThereWaypointsToFollow()
        {
            return _currentWaypoint < _path.vectorPath.Count;
        }


        private bool IsWaypointReached(Transform enemyTransform)
        {
            float distance = Vector2.Distance(enemyTransform.position, _path.vectorPath[_currentWaypoint]);
            return distance < _nextWaypointDistance;
        }
        

        private void Start()
        {
            _seeker = GetComponent<Seeker>();
        }
    }
}
