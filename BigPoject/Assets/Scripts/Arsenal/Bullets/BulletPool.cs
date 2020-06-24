using System.Collections.Generic;
using UnityEngine;

namespace Arsenal.Bullets
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet = null;
        [SerializeField] private int _bulletsAmount = 0;
        private List<GameObject> _bulletsPool;
        public static BulletPool SharedInstance;
        
        private void Awake()
        {
            SharedInstance = this;
        }

    
        private void Start()
        {
            // Filling pool with bullets.
            _bulletsPool = new List<GameObject>();
            for (int i = 0; i < _bulletsAmount; i++)
            {
                GameObject obj = (GameObject)Instantiate(_bullet);
                obj.SetActive(false);
                _bulletsPool.Add(obj);
                obj.transform.SetParent(this.transform);
            }
        }

    
        // Get bullet if it is available.
        public GameObject GetBullet()
        {
            for (int i = 0; i < _bulletsPool.Count; i++)
            {
                if (!_bulletsPool[i].activeInHierarchy)
                {
                    return _bulletsPool[i];
                }
            }
        
            return null;
        }
    }
}
