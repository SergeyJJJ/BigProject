using System.Collections.Generic;
using UnityEngine;

namespace ForItemsAndCreatures
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _object = null;                 // Object with which the pool will be filled
        [SerializeField] private int _poolAmount = 0;                       // Objects amount in the pool.
        private List<GameObject> _objectsPool = new List<GameObject>();     // List that contains all of objects.
        public static ObjectPool SharedInstance;                            // Static link for using this pool.
        
        
        public GameObject GetPooledObject()
        {
            // Go through the pool and find nonactive object.
            // Return object if found, else return null.
            
            GameObject receivedObject = null;  
            
            for (int objectIndex = 0; objectIndex < _poolAmount; objectIndex++)
            {
                if (!_objectsPool[objectIndex].activeInHierarchy)
                {
                    receivedObject = _objectsPool[objectIndex];
                }
            }
            
            return receivedObject;
        }
        
        
        private void CreateObjectPool()
        {
            // Instantiate objects as child objects of the
            // corresponding objects pool gameObject,
            // deactivate object and add to the object pool list.
            
            for (int objectIndex = 0; objectIndex < _poolAmount; objectIndex++)
            {
                GameObject newObject = Instantiate(_object, Vector3.zero, Quaternion.identity);
                newObject.SetActive(false);
                newObject.transform.SetParent(this.transform);
                _objectsPool.Add(newObject);
            }
        }
        
        
        private void Awake()
        {
            SharedInstance = this;
            CreateObjectPool();
        }
    }
}
