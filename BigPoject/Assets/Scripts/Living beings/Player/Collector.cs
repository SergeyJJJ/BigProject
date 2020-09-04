using System;
using Environment.InterfacesOfUsing;
using UnityEngine;

namespace Living_beings.Player
{
    public class Collector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            // If object derive form ICollectable interface
            // use its Collect() method.
            ICollectable collectableObject = other.gameObject.GetComponent<ICollectable>();
            collectableObject?.Collect();
        }
    }
}
