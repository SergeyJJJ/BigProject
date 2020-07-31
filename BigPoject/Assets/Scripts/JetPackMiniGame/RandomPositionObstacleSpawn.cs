using System;
using UnityEngine;
using Random = System.Random;

namespace JetPackMiniGame
{
    public class RandomPositionObstacleSpawn : MonoBehaviour
    {
        [SerializeField] private float _workingTime = 0f;                // Determine how long obstacles will be spawned.
        [SerializeField] private float _timeBetweenEachObstacle = 0f;    // Time that must pass before a new obstacle will be spawned.
        private float _newObstacleSpawnTimer = 0f;                       // Timer that controls time between obstacles spawn.
        
        [Serializable] private class SpawnBoundaries                    // Struct that contains obstacle spawn boundaries.
        {
            public int leftBoundary = 0;
            public int rightBoundary = 0;
            public int downBoundary = 0;
            public int upBoundary = 0;
        }
        [SerializeField] private SpawnBoundaries _spawnConstraints = null;

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
                    AdjacentObstaclesCheck adjacentObstaclesCheck = obstacle.GetComponent<AdjacentObstaclesCheck>();

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

                        } while ((adjacentObstaclesCheck.IsIntersectAnotherObstacle == true) &&
                                 (positionFindingAttemptsCounter <= positionSearchAttemptsLimit));
                    }
                }
            }
        }


        private Vector3 GetRandomSpawnPosition()
        {
            Random random = new Random();
            
            float xRandomSpawnPosition = random.Next(_spawnConstraints.leftBoundary, _spawnConstraints.rightBoundary + 1);
            float yRandomSpawnPosition = random.Next(_spawnConstraints.downBoundary, _spawnConstraints.upBoundary + 1);
    
            return new Vector2(xRandomSpawnPosition, yRandomSpawnPosition);
        }
    }
}