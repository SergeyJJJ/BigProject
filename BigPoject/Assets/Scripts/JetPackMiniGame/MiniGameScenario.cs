﻿using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JetPackMiniGame
{
    public class MiniGameScenario : MonoBehaviour
    {
        [Header("Pools of obstacles")]
        [SerializeField] private ObstaclePool _singlePlatformsPool = null;
        [SerializeField] private ObstaclePool _movingSinglePlatformsPool = null;
        [SerializeField] private ObstaclePool _singleGatesPool = null;
        [SerializeField] private ObstaclePool _doubleGatesPool = null;

        private delegate void SpawnObstacleDelegate();                                                // Delegate that can contain obstacle spawn method.
        private readonly SpawnObstacleDelegate[] _obstacleSpawnTypes = new SpawnObstacleDelegate[4];  // Array contains collection of different obstacle spawn methods.

        private readonly int[][] _obstacleSpawnProbabilities = { new int[] {100, 0, 0, 0},        // Array where each sub-array contains probability distribution of obstacle spawn.
                                                                 new int[] {80, 20, 0, 0},                 // Important: sum of elements in every sub-array must be equal to one hundred.
                                                                 new int[] {40, 60, 0, 0},
                                                                 new int[] {10, 90, 0, 0},
                                                                 new int[] {10, 80, 10, 0},
                                                                 new int[] {10, 60, 30, 0},
                                                                 new int[] {10, 40, 50, 0},
                                                                 new int[] {10, 10, 80, 0},
                                                                 new int[] {10, 10, 70, 10},
                                                                 new int[] {10, 10, 50, 30},
                                                                 new int[] {10, 10, 10, 70} };

        private void Awake()
        {
            _obstacleSpawnTypes[0] = SpawnSinglePlatform;
            _obstacleSpawnTypes[1] = SpawnMovingSinglePlatform;
            _obstacleSpawnTypes[2] = SpawnSingleGates;
            _obstacleSpawnTypes[3] = SpawnDoubleGates;
        }


        private void Start()
        {
            StartCoroutine(MiniGameRoutine());
        }


        // Main coroutine that control MiniGame scenario
        // execution sequence.
        private IEnumerator MiniGameRoutine()
        {
            // Get spawn function from array of functions.
            // Spawn function received randomly based on
            // specific probability distribution.
            SpawnObstacleDelegate spawnFunction = GetRandomSpawnFunction(_obstacleSpawnProbabilities[0]);

            // Use current spawn function to produce an obstacle.
            if (spawnFunction != null)
            {
                spawnFunction();
            }

            yield return new WaitForSeconds(2f);
        }


        // Use to get spawn function pseudo-randomly based on
        // certain probability distribbution.
        private SpawnObstacleDelegate GetRandomSpawnFunction(int[] probabilityDistribution)
        {
            int randomValue = (int) (Random.value * 100);
            
            // Represented by a sum of probabilities.
            int currentProbability = 0;

            // Go through all probabilities and check if our
            // randomValue is within current probability. If it is
            // use index of current probability to get access to function
            // with corresponding index.
            for (int probabilityIndex = 0; probabilityIndex < probabilityDistribution.Length; probabilityIndex++)
            {
                currentProbability += probabilityDistribution[probabilityIndex];

                if (randomValue <= currentProbability)
                {
                    Debug.Log($"Random value: {randomValue}; index: {probabilityIndex};");
                    return _obstacleSpawnTypes[probabilityIndex];
                }
            }
            
            // Will happen if the input's probabilities sums to less than 100.
            return null;
        }
        

        private void SpawnSinglePlatform()
        {
            float leftBoundary = -10f;
            float rightBoundary = 10f;
            ObstacleSpawner.RandomSpawnOnXAxisRange(_singlePlatformsPool, leftBoundary, rightBoundary);
        }


        private void SpawnMovingSinglePlatform()
        {
            Vector2 spawnPosition = new Vector2(0f, 25f);
            ObstacleSpawner.SpawnObstacleOnPosition(_movingSinglePlatformsPool, spawnPosition);
        }


        private void SpawnSingleGates()
        {
            float leftBoundary = -10f;
            float rightBoundary = 10f;
            ObstacleSpawner.RandomSpawnOnXAxisRange(_singleGatesPool, leftBoundary, rightBoundary);
        }


        private void SpawnDoubleGates()
        {
            float leftBoundary = -5f;
            float rightBoundary = 5f;
            ObstacleSpawner.RandomSpawnOnXAxisRange(_doubleGatesPool, leftBoundary, rightBoundary);
        }
    }
}