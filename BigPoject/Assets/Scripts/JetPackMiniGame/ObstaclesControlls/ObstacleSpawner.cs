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

        // Method that spawns an obstacle on random position
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
        
        
        // Method that spawns an obstacle on specific position.
        public static void SpawnOnPosition(ObstaclePool obstaclePool, Vector2 spawnPosition)
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
        
        
        // Method that spawns an obstacle randomly facing
        // to the left or to the right.
        public static void SpawnOnPositionFacingOnRandomXAxisSide(ObstaclePool obstaclePool, Vector2 spawnPosition)
        {
            GameObject obstacle = obstaclePool.GetPooledObstacle();

            // If obstacle was received from the pool.
            if (obstacle != null)
            {
                // Place received obstacle on received position.
                obstacle.transform.position = spawnPosition;

                // Turn obstacle to the random horizontal facing side.
                TurnToRandomHorizontalFacingSide(obstacle);

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


        private static void TurnToRandomHorizontalFacingSide(GameObject obstacle)
        {
            float[] facingRotations = {0, 180};
            int randomFacingIndex = Random.Range(0, facingRotations.Length);
            
            Debug.Log(randomFacingIndex);
            
            obstacle.transform.Rotate(new Vector2(0, facingRotations[randomFacingIndex]));
        }
    }
}