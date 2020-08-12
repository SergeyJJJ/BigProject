using System.Collections.Generic;
using UnityEngine;

namespace Environment.ObstaclesBehaviour
{
    public class WindZone : MonoBehaviour
    {
        [SerializeField] private float _windForce = 0;                                                             // Amount of force that applied to the blowable objects in the wind zone.
        [SerializeField] private LayerMask _blowableByWind = Physics2D.AllLayers;                                  // What can be blown away by the wind.
    
        private Dictionary<GameObject, Rigidbody2D> _blowableObjects = new Dictionary<GameObject, Rigidbody2D>();  // List contains objects to which an be applied force(wind).
    
        private Vector2 _windForceDirection = Vector2.zero;

        private void Start()
        {
            Vector2 transformsLeft = -transform.right;
            _windForceDirection = transformsLeft * _windForce;
        }
    
    
        private void FixedUpdate()
        {
            foreach (var blowableObject in _blowableObjects)
            {
                blowableObject.Value.AddForce(_windForceDirection, ForceMode2D.Impulse);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsCanBeBlownByWind(other))
            {
                _blowableObjects.Add(other.gameObject, other.GetComponent<Rigidbody2D>());
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsCanBeBlownByWind(other))
            {
                GameObject exitedGameObject = other.gameObject;
            
                if (_blowableObjects.ContainsKey(exitedGameObject))
                {
                    _blowableObjects.Remove(exitedGameObject);
                }
            }
        }


        private bool IsCanBeBlownByWind(Collider2D someObject)
        {
            return ((1 << someObject.gameObject.layer) & _blowableByWind) != 0;
        }
    }
}
