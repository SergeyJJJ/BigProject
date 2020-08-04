using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JetPackMiniGame
{
    // Class that contains methods to spawn obstacles in
    // different ways.
    public static class ObstacleSpawner
    {
        private const float DefaultVerticalSpawnPosition = 25f;

        // Method that spawns obstacle on random position
        // within specific range.
        public static void RandomSpawnOnXAxisRange(ObstaclePool obstaclePool, float leftBoundary, float rightBoundary)
        {
            GameObject obstacle = obstaclePool.GetPooledObstacle();

            // If obstacle was received from the pool.
            if (obstacle != null)
            {
                // Get random position to place an obstacle.
                Vector2 spawnPosition = GetRandomPositionOnXAxisRange(leftBoundary, rightBoundary);

                // Place received obstacle on received position.
                obstacle.transform.position = spawnPosition;

                // Activate obstacle.
                obstacle.SetActive(true);
            }
        }
        
        
        // Method that spawns obstacle on specific position.
        public static void SpawnObstacleOnPosition(ObstaclePool obstaclePool, Vector2 spawnPosition)
        {
            GameObject obstacle = obstaclePool.GetPooledObstacle();

            // If obstacle was received from the pool.
            if (obstacle != null)
            {
                // Place received obstacle on received position.
                obstacle.transform.position = spawnPosition;

                // Activate obstacle.
                obstacle.SetActive(true);
            }
        }
        
        
        private static Vector3 GetRandomPositionOnXAxisRange(float leftBoundary, float rightBoundary)
        {
            float xRandomSpawnPosition = Random.Range(leftBoundary, rightBoundary);
            float ySpawnPosition = DefaultVerticalSpawnPosition;
    
            return new Vector2(xRandomSpawnPosition, ySpawnPosition);
        }
    }
}
