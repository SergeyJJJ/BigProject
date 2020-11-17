using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment.JetPackObstaclesBehaviour.JetPackObstacleControl
{
    public class ObstacleSpawnScenario : MonoBehaviour
    {
        [Header("Pools of obstacles")]
        [SerializeField] private ObstaclePool _singlePlatformsPool = null;
        [SerializeField] private ObstaclePool _movingSinglePlatformsPool = null;
        [SerializeField] private ObstaclePool _singleGatesPool = null;
        [SerializeField] private ObstaclePool _spinningPlatfromsPool = null;
        [SerializeField] private ObstaclePool _doubleGatesPool = null;
        [SerializeField] private ObstaclePool _lazersPool = null;
        [SerializeField] private ObstaclePool _ventilatorsPool = null;

        private delegate void SpawnObstacleDelegate();                                                // Delegate that can contain obstacle spawn method.
        private readonly SpawnObstacleDelegate[] _obstacleSpawnTypes = new SpawnObstacleDelegate[7];  // Array contains collection of different obstacle spawn methods.

        private readonly int[][] _obstacleSpawnProbabilities = { new int[] {100, 0, 0, 0, 0, 0, 0},   // Array where each sub-array contains probability distribution of obstacle spawn types.
                                                                 new int[] {80, 20, 0, 0, 0, 0, 0},   // Important: sum of elements in every sub-array must be equal to one hundred.
                                                                 new int[] {40, 60, 0, 0, 0, 0, 0},
                                                                 new int[] {10, 90, 0, 0, 0, 0, 0},
                                                                 new int[] {10, 50, 40, 0, 0, 0, 0},
                                                                 new int[] {10, 10, 80, 0, 0, 0, 0},
                                                                 new int[] {10, 10, 60, 20, 0, 0, 0},
                                                                 new int[] {10, 10, 30, 50, 0, 0, 0},
                                                                 new int[] {10, 10, 10, 60, 10, 0, 0},
                                                                 new int[] {10, 10, 10, 10, 60, 0, 0},
                                                                 new int[] {10, 10, 0, 0, 0, 80, 0},
                                                                 new int[] {0, 0, 0, 0, 0, 0, 100} };

        private int[] _currentProbabilityDistribution = new int[7];

        private void Awake()
        {
            _obstacleSpawnTypes[0] = SpawnSinglePlatform;
            _obstacleSpawnTypes[1] = SpawnMovingSinglePlatform;
            _obstacleSpawnTypes[2] = SpawnSingleGates;
            _obstacleSpawnTypes[3] = SpawnDoubleGates;
            _obstacleSpawnTypes[4] = SpawnSpinningPlatform;
            _obstacleSpawnTypes[5] = SpawnLazer;
            _obstacleSpawnTypes[6] = SpawnVentiator;
        }


        private void Start()
        {
            _currentProbabilityDistribution = _obstacleSpawnProbabilities[0];

            StartSpawnObstacles();
        }


        private void StartSpawnObstacles()
        {
            StartCoroutine(ChangeProbabilityDistributionRoutine());
            StartCoroutine(SpawnObstaclesRoutine());   
        }


        private void StopSpawnObstacles()
        {
            StopAllCoroutines();
        }
        
        
        // Coroutine that control changing of probability distribution
        // of obstacle spawn types.
        private IEnumerator ChangeProbabilityDistributionRoutine()
        {
            float timeBeforeDistributionChange = 18f;
            
            // Wait some time before start probability distribution changing.
            yield return new WaitForSeconds(5f);
            
            for (int distributionIndex = 1; distributionIndex < _obstacleSpawnProbabilities.Length; distributionIndex++)
            {
                _currentProbabilityDistribution = _obstacleSpawnProbabilities[distributionIndex];
                yield return new WaitForSeconds(timeBeforeDistributionChange);
            }
            
            // Stop spawn obstacles after all probability distributions were used.
            StopSpawnObstacles();
        }


        // Coroutine that control spawning of obstacles.
        private IEnumerator SpawnObstaclesRoutine()
        {
            // Get spawn function from array of functions.
            // Spawn function received randomly based on
            // specific probability distribution.
            float timeBeforeSpawnNewObstacle = 3f;

            // Wait some time before spawning obstacles.
            yield return new WaitForSeconds(10f);
            
            while (true)
            {
                SpawnObstacleDelegate spawnFunction = GetRandomSpawnFunction(_currentProbabilityDistribution);

                // Use current spawn function to produce an obstacle.
                spawnFunction?.Invoke();

                yield return new WaitForSeconds(timeBeforeSpawnNewObstacle);
            }
        }
        

        // Use to get spawn function pseudo-randomly based on
        // certain probability distribution.
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
            ObstacleSpawner.SpawnOnPosition(_movingSinglePlatformsPool, spawnPosition);
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


        private void SpawnSpinningPlatform()
        {
            float leftBoundary = -7f;
            float rightBoundary = 7f;
            ObstacleSpawner.RandomSpawnOnXAxisRange(_spinningPlatfromsPool, leftBoundary, rightBoundary);
        }


        private void SpawnLazer()
        {
            float leftBoundary = -7f;
            float rightBoundary = 7f;
            ObstacleSpawner.RandomSpawnOnXAxisRange(_lazersPool, leftBoundary, rightBoundary);
        }


        private void SpawnVentiator()
        {
            Vector2 spawnPosition = new Vector2(0f, 25f);
            ObstacleSpawner.SpawnOnPositionFacingOnRandomXAxisSide(_ventilatorsPool, spawnPosition);
        }
    }
}