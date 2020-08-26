using System;
using UnityEngine;

namespace Living_beings.Player
{
    public class Collector : MonoBehaviour
    {
        [SerializeField] private LayerMask _collectable = Physics2D.AllLayers;              // Layers that determine which objects can be collected. 
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            // If object is derive form ICollectable interface
            // use its Collect() method.
            if (IsObjectCollectable(other.gameObject))
            {
                ICollectable collectableObject = other.gameObject.GetComponent<ICollectable>();
                collectableObject.Collect();
            }
        }


        private bool IsObjectCollectable(GameObject collidedObject)
        {
            return ((1 << collidedObject.gameObject.layer) & _collectable) != 0;
        }
    }
}
