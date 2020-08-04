using System.Collections.Generic;
using UnityEngine;

namespace JetPackMiniGame
{
    public class MiniGameScenario : MonoBehaviour
    {
        [Header("Pools of obstacles")]
        [SerializeField] private ObstaclePool _singlePlatformsPool = null;
        [SerializeField] private ObstaclePool _movingSinglePlatformsPool = null;
        [SerializeField] private ObstaclePool _singleGatesPool = null;
        [SerializeField] private ObstaclePool _doubleGatesPool = null;

        private delegate void SpawnObstacleDelegate();              // Delegate that can contain obstacle spawn method.
        private SpawnObstacleDelegate[] _obstacleSpawnTypes = { };  // Array contains collection of different obstacle spawn methods.


        private void SpawnSinglePlatform()
        {
            
        }
    }
}