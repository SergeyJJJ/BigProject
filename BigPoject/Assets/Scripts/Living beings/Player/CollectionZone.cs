using Environment.InterfacesOfUsing;
using UnityEngine;

namespace Living_beings.Player
{
    public class CollectionZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            ICollectable collectable = other.GetComponent<ICollectable>();
            collectable?.Collect();
        }
    }
}
