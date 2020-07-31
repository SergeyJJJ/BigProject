using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JetPackMiniGame
{
    public class RandomPositionObstacleSpawn : MonoBehaviour
    {
        [SerializeField] private float _workingTime = 0f;                // Determine how long obstacles will be spawned.
        [SerializeField] private float _timeBetweenEachObstacle = 0f;    // Time that must pass before a new obstacle will be spawned.
        private float _newObstacleSpawnTimer = 0f;                       // Timer that controls time between obstacles spawn.
        
        [Serializable] private struct SpawnBoundaries                    // Struct that contains obstacle spawn boundaries.
        {
            public float leftBoundary;
            public float rightBoundary;
            public float upBoundary;
            public float downBoundary;
        }
        [SerializeField] private SpawnBoundaries SpawnConstraints;
        

        private void Start()
        {
            SpawnObstacles(_workingTime);
        }


        public void SpawnObstacles(float workingTime)
        {
            // While working time for spawning obstacle did not run out.
            while (0 < workingTime)
            {
                // Decrease working time.
                workingTime -= Time.deltaTime;

                // After certain amount of time
                if (_newObstacleSpawnTimer <= 0)
                {
                    // Get obstacle from pool.
                    GameObject obstacle;
                    obstacle = ObstaclePool.SharedInstance.GetPooledObstacle();
                    
                    // If obstacle was received from the pool.
                    if (obstacle != null)
                    {
                        // Do, while the obstacle will be placed to the position
                        // that doesn`t instersects with another obstacles. Or while
                        // attempts of finding clear position is not run out.
                        int positionFindingAttemptsCounter = 0;
                        const int positionSearchAttemptsLimit = 20;
                        do
                        {
                            // Get random position to place an obstacle.
                            Vector2 spawnPosition = GetRandomSpawnPosition();

                            // Place received obstacle on received position.
                            obstacle.transform.position = spawnPosition;

                            // Activate obstacle.
                            obstacle.SetActive(true);

                            // Increase position finding attempts counter.
                            positionFindingAttemptsCounter++;

                            // DEVELOPMENT PROCESS!!!
                            break;

                        } while (positionFindingAttemptsCounter <= positionSearchAttemptsLimit);
                    }
                }
            }
        }
        
        
        private Vector3 GetRandomSpawnPosition()
        {
            float xRandomSpawnPosition = Random.Range(SpawnConstraints.leftBoundary, SpawnConstraints.rightBoundary);
            float yRandomSpawnPosition = Random.Range(SpawnConstraints.downBoundary, SpawnConstraints.upBoundary);
    
            return new Vector2(xRandomSpawnPosition, yRandomSpawnPosition);
        }
    }
}