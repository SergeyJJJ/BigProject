using System.Collections;
using UnityEngine;

namespace JetPackMiniGame
{
    public class MiniGameScenario : MonoBehaviour
    {
        [Header("Stages execution periods")]
        [SerializeField] private float[] _executionPeriods = null;                                   // Define until what time specific spawn templates will be performed: seconds.

        [Space, Header("Obstacles pools")]
        [SerializeField] private ObstaclePool _singlePlatformPool = null;                            // Contains pool of single platform obstacles.
        [SerializeField] private ObstaclePool _gatesPool = null;                                     // Contains pool of gates obstacles.
        [SerializeField] private ObstaclePool _movingSinglePlatformPool = null;                      // Contains pool of moving single platform obstacles.
        
        private RandomPositionObstacleSpawn _singlePlatformSpawn = null;                             // Contains reference to the specific spawn template.
        private RandomPositionObstacleSpawn _gatesSpawn = null;                                      // Contains reference to the specific spawn template.      
        private RandomPositionObstacleSpawn _movingSinglePlatformSpawn = null;                             // Contains reference to the specific spawn template.
        
        private float _executingTime = 0;                                                            // Time that controls what patterns will be performed at certain period of time.

        private void Awake()
        {
            _singlePlatformSpawn = new RandomPositionObstacleSpawn(_singlePlatformPool);
            _gatesSpawn = new RandomPositionObstacleSpawn(_gatesPool);
            _movingSinglePlatformSpawn = new RandomPositionObstacleSpawn(_movingSinglePlatformPool);
        }
    }
}