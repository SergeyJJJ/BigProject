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

        private ObstaclePool _obstaclePool = null;                           // Pool with obstacles to spawn.
        
        
        public RandomPositionObstacleSpawn(ObstaclePool obstaclePool)
        {
            _obstaclePool = obstaclePool;
        }


        public void SpawnObstacle()
        {
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


        private Vector3 GetRandomSpawnPosition()
        {
            Random random = new Random();
            
            float xRandomSpawnPosition = random.Next(_horizontalSpawnBoundaries.leftBoundary, _horizontalSpawnBoundaries.rightBoundary + 1);
            float ySpawnPosition = _verticalSpawnPosition;
    
            return new Vector2(xRandomSpawnPosition, ySpawnPosition);
        }
    }
}