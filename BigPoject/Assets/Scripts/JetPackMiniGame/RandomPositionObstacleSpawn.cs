using System;
using UnityEngine;
using Random = System.Random;

namespace JetPackMiniGame
{
    public class RandomPositionObstacleSpawn : MonoBehaviour
    {
        [Serializable] private class HorizontalSpawnBoundaries                                // Struct that contains obstacle spawn boundaries on X-Axis.
        {
            public int leftBoundary = 0;
            public int rightBoundary = 0;
        }
        
        [SerializeField] private float _workingTime = 0f;                                     // Determine how long obstacles will be spawned.
        [SerializeField] private float _timeBetweenEachObstacle = 0f;                         // Time that must pass before a new obstacle will be spawned.
        private float _newObstacleSpawnTimer = 0f;                                            // Timer that controls time between obstacles spawn.
        
        [Space]
        [SerializeField] private int _verticalSpawnPosition = 0;                              // Position on Y-Axis where obstacles will be spawned.
        [SerializeField] private HorizontalSpawnBoundaries _horizontalSpawnBoundaries = null; // Instance horizontal spawn boundaries data type, used to get access to individual fields.

        public void SpawnObstacles(float workingTime)
        {
            _workingTime = workingTime;
        }


        private void Start()
        {
            SpawnObstacles(_workingTime);
        }


        private void Update()
        {
            // While working time for spawning obstacle did not run out.
            if (0 < _workingTime)
            {
                _workingTime -= Time.deltaTime;
                _newObstacleSpawnTimer -= Time.deltaTime;

                // After certain amount of time, spawn obstacle.
                if (_newObstacleSpawnTimer <= 0)
                {
                    _newObstacleSpawnTimer = _timeBetweenEachObstacle;

                    GameObject obstacle = ObstaclePool.SharedInstance.GetPooledObstacle();

                    // If obstacle was received from the pool.
                    if (obstacle != null)
                    {
                        // Get random position to place an obstacle.
                        Vector2 spawnPosition = GetRandomSpawnPosition();

                        // Place received obstacle on received position.
                        obstacle.transform.position = spawnPosition;

                        // Activate obstacle.
                        obstacle.SetActive(true);
                    }
                }
            }
        }


        private Vector3 GetRandomSpawnPosition()
        {
            Random random = new Random();
            
            float xRandomSpawnPosition = random.Next(_horizontalSpawnBoundaries.leftBoundary, _horizontalSpawnBoundaries.rightBoundary + 1);
            float ySpawnPosition = _verticalSpawnPosition;
    
            return new Vector2(xRandomSpawnPosition, ySpawnPosition);
        }
    }
}