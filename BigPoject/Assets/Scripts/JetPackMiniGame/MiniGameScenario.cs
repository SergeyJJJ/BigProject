using System;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace JetPackMiniGame
{
    public class MiniGameScenario : MonoBehaviour
    {
        [Header("Stages execution periods")]
        [SerializeField] private float[] _executionPeriods = null;                                   // Define until what time specific spawn templates will be performed: seconds.

        [Space, Header("Obstacles pools")]
        [SerializeField] private ObstaclePool _singlePlatformPool = null;                            // Contains pool of single platform obstacles.
        [SerializeField] private ObstaclePool _gatesPool = null;                                     // Contains pool of gates obstacles.
        
        private RandomPositionObstacleSpawn _singlePlatformSpawn = null;                             // Contains reference to the specific spawn template.
        private RandomPositionObstacleSpawn _gatesSpawn = null;                                      // Contains reference to the specific spawn template.      
        
        private float _executingTime = 0;                                                            // Time that controls what patterns will be performed at certain period of time.

        private void Awake()
        {
            _singlePlatformSpawn = new RandomPositionObstacleSpawn(_singlePlatformPool);
            _gatesSpawn = new RandomPositionObstacleSpawn(_gatesPool);
        }
        
        
        private void Start()
        {
            
        }
        
        
        private void Update()
        {
            //throw new NotImplementedException();
            // Do not control spawn frequency from spawn script,
            // you must to control it from this script using coroutines.
        }
    }
}