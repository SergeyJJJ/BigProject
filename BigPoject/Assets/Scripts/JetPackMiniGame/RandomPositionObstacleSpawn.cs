using System;
using UnityEngine;
using Random = System.Random;

namespace JetPackMiniGame
{
    public class RandomPositionObstacleSpawn
    {
        private class HorizontalSpawnBoundaries                              // Struct that contains obstacle spawn boundaries on X-Axis.
        {
            public int leftBoundary = -10;
            public int rightBoundary = 10;
        }
        
        private HorizontalSpawnBoundaries _horizontalSpawnBoundaries = new HorizontalSpawnBoundaries(); // Instance horizontal spawn boundaries data type, used to get access to individual fields.
        private int _verticalSpawnPosition = 25;                             // Position on Y-Axis where obstacles will be spawned.

        private float _timeBetweenEachObstacle = 0f;                         // Time that must pass before a new obstacle will be spawned.
        private float _newObstacleSpawnTimer = 0f;                           // Timer that controls time between obstacles spawn.

        private ObstaclePool _obstaclePool = null;                           // Pool with obstacles to spawn.
        
        
        #region Properties

        public float TimeBetweenEachObstacle
        {
            get => _timeBetweenEachObstacle;
            set => _timeBetweenEachObstacle = value;
        }

        #endregion Properties
        
        
        public RandomPositionObstacleSpawn(float timeBetweenEachObstacle, ObstaclePool obstaclePool)
        {
            _timeBetweenEachObstacle = timeBetweenEachObstacle;
            _obstaclePool = obstaclePool;
        }
        
        
        public void SpawnObstacles()
        {
            _newObstacleSpawnTimer -= Time.deltaTime;

            // After certain amount of time, spawn obstacle.
            if (_newObstacleSpawnTimer <= 0)
            {
                _newObstacleSpawnTimer = _timeBetweenEachObstacle;

                GameObject obstacle = _obstaclePool.GetPooledObstacle();

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
        

        private Vector3 GetRandomSpawnPosition()
        {
            Random random = new Random();
            
            float xRandomSpawnPosition = random.Next(_horizontalSpawnBoundaries.leftBoundary, _horizontalSpawnBoundaries.rightBoundary + 1);
            float ySpawnPosition = _verticalSpawnPosition;
    
            return new Vector2(xRandomSpawnPosition, ySpawnPosition);
        }
    }
}