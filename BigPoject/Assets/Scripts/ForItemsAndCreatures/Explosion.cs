using UnityEngine;

namespace ForItemsAndCreatures
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private Transform _explosionCenterPosition = null;            // Explosion center point. Used as a point from which force will be applied.
        [SerializeField] private float _explosionForce = 0;                            // Explosion force.
        [SerializeField] private float _explosionRadius = 0f;                          // Explosion radius.
        [SerializeField] private LayerMask _affectedByExplosion = Physics2D.AllLayers; // What can be affect by explosion.
        
        
        public void Explode()
        {
            Collider2D[] affectedColliders = Physics2D.OverlapCircleAll(_explosionCenterPosition.position, _explosionRadius, _affectedByExplosion);
            
            foreach (Collider2D collider in affectedColliders)
            {
                Rigidbody2D rigidbody = collider.GetComponent<Rigidbody2D>();
                Vector2 forceDirection = (collider.transform.position - _explosionCenterPosition.position).normalized;
                Debug.Log(collider.name + " " + collider.transform.position);
                
                if (rigidbody != null)
                {
                    rigidbody.AddForce(forceDirection * _explosionForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
