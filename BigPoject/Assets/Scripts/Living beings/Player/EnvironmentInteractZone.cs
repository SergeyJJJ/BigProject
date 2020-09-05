using System;
using Environment.InterfacesOfUsing;
using UnityEngine;

namespace Living_beings.Player
{
    public class EnvironmentInteractZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check from which interface object is derived to 
            // use appropriate functionality.
            
            IInteractable interactable = other.GetComponent<IInteractable>();
            interactable?.Interact();

            ICollectable collectable = other.GetComponent<ICollectable>();
            collectable?.Collect();
        }
    }
}
