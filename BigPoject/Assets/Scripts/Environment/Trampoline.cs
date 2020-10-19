using System;
using UnityEngine;

namespace Environment
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private LayerMask _isCanBeBouced = Physics2D.AllLayers;     // Define what can be bounced.
        [SerializeField] private float _bounceForce = 0;                             // How much force will be applied to the object.
        
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            // If something can be bounced form trampoline
            // throw it away.
            if (IsCanBeBounced(other.gameObject))
            {
                if (other.rigidbody != null)
                {
                    BounceObject(other.rigidbody);
                }
            }
        }


        private bool IsCanBeBounced(GameObject collidedGameObject)
        {
            return ((1 << collidedGameObject.layer) & _isCanBeBouced) != 0;
        }


        private void BounceObject(Rigidbody2D rigidBody2DComponent)
        {
            Vector2 forceDirection = Vector2.up;
            
            rigidBody2DComponent.AddForce(forceDirection * _bounceForce, ForceMode2D.Impulse);
        }
    }
}
