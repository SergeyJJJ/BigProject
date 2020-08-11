using System.Collections.Generic;
using UnityEngine;

namespace Environment.ObstaclesBehaviour
{
    public class WindZoneObstacle : MonoBehaviour
    {
        [SerializeField] private float _windForce = 0;                                                             // Amount of force that applied to the blowable objects in the wind zone.
        [SerializeField] private float _residualForce = 0;                                                         // Amount of force that applied to the blowable objects after they left the wind zone.
        [SerializeField] private LayerMask _blowableByWind = Physics2D.AllLayers;                                  // What can be blown away by the wind.
        
        private Dictionary<GameObject, Rigidbody2D> _blowableObjects = new Dictionary<GameObject, Rigidbody2D>();  // List contains objects to which an be applied force(wind).
        
        private Vector2 _windForceDirection = Vector2.zero;
        private Vector2 _residualForceDirection = Vector2.zero;
        
        private void Start()
        {
            Vector2 transformsLeft = -transform.right;
            _windForceDirection = transformsLeft * _windForce;
            _residualForceDirection = transformsLeft * _residualForce;
        }
        
        
        private void FixedUpdate()
        {
            foreach (var blowableObject in _blowableObjects)
            {
                blowableObject.Value.AddForce(_windForceDirection, ForceMode2D.Force);
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
                    Debug.Log("Blow");
                    _blowableObjects[exitedGameObject].AddForce(_residualForceDirection, ForceMode2D.Force);
                    _blowableObjects.Remove(exitedGameObject);
                    
                    
                    //////////////////Works only once for a short time.
                }
            }
        }


        private bool IsCanBeBlownByWind(Collider2D someObject)
        {
            return ((1 << someObject.gameObject.layer) & _blowableByWind) != 0;
        }
    }
}
