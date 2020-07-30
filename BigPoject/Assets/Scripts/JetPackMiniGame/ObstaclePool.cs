using System;
using System.Collections.Generic;
using UnityEngine;

namespace JetPackMiniGame
{
    public class ObstaclePool : MonoBehaviour
    {
        private List<GameObject> _obstaclePool = new List<GameObject>();      // List that contains nonactive obstacles.
        [SerializeField] private int _poolAmount = 0;                         // Obstacles amount in the pool.
        [SerializeField] private GameObject _obstacle = null;                 // Obstacles with which the pool will be filled 
        
        private static ObstaclePool _sharedInstance = null;                   // Reference to the current obstacles pool.

        #region Properties

        public ObstaclePool SharedInstance => _sharedInstance;

        #endregion Properties

        private void Awake()
        {
            _sharedInstance = this;
        }

        
        private void Start()
        {
            CreateObstaclePool();
        }

        
        private void CreateObstaclePool()
        {
            // Instantiate obstacles as child objects of the
            // corresponding obstacles pool gameObject,
            // deactivate it and add to the obstacles pool list.
            
            for (int obstacleIndex = 0; obstacleIndex < _poolAmount; obstacleIndex++)
            {
                GameObject newObstacle;
                newObstacle = Instantiate(_obstacle, Vector3.zero, Quaternion.identity);
                newObstacle.SetActive(false);
                newObstacle.transform.SetParent(_sharedInstance.transform);
                _obstaclePool.Add(newObstacle);
            }
        }

        
        private GameObject GetPooledObstacle()
        {
            // Go through the pool and find nonactive obstacle.
            // Return obstacle if found, else return null.
            
            GameObject receivedObstacle = null;  
            
            for (int obstacleIndex = 0; obstacleIndex < _poolAmount; obstacleIndex++)
            {
                if (!_obstaclePool[obstacleIndex].activeInHierarchy)
                {
                    receivedObstacle = _obstaclePool[obstacleIndex];
                }
            }
            
            return receivedObstacle;
        }
    }
}
