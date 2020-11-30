using System;
using Arsenal.WeaponsProjectiles.ProjectilesBehaviour;
using Arsenal.WeaponsProjectiles.ProjectilesData;
using Environment.ThingsDestruction;
using LivingBeings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ForItemsAndCreatures
{
    public class Explosion : MonoBehaviour
    {
        [Serializable]
        public struct TorqueLimits
        {
            public float minTorque;
            public float maxTorque;
        }

        [SerializeField] private float _damage = 0;                                    // Explosion damage.
        [SerializeField] private float _force = 0;                                     // Explosion force.
        [SerializeField] private float _radius = 0f;                                   // Explosion radius.
        [SerializeField] private Transform _centerPoint = null;                        // Explosion center point. Used as a point from which force will be applied.
        [SerializeField] private TorqueLimits _torqueLimits = default(TorqueLimits);   // Minimum and maximum torque of pieces.
        [SerializeField] private LayerMask _affectedByExplosion = Physics2D.AllLayers; // What can be affect by explosion.
        [SerializeField] private GameObject _explosionVisualEffects = null;            // Used to play explosion animation if it is exist. 

        public void Initialize(LayerMask affectedByExplosion, float damage, float force = 0f, float radius = 0f,
                               Transform centerPoint = null, Rocket.TorqueLimits torqueLimits = default(Rocket.TorqueLimits))
        {
            _affectedByExplosion = affectedByExplosion;
            _damage = damage;
            _force = force;
            _radius = radius;
            _centerPoint = centerPoint;
            _torqueLimits.maxTorque = torqueLimits.maxTorque;
            _torqueLimits.minTorque = torqueLimits.minTorque;
            _affectedByExplosion = affectedByExplosion;
        }


        public void Initialize(Rocket rocket)
        {
            _affectedByExplosion = rocket.HittableObjects;
            _damage = rocket.Damage;
            _force = rocket.ExplosionForce;
            _radius = rocket.ExplosionRadius;
            _centerPoint = transform;
            _torqueLimits.maxTorque = rocket.ExplosionTorqueLimits.maxTorque;
            _torqueLimits.minTorque = rocket.ExplosionTorqueLimits.minTorque;
        }
        
        
        // Simulate an explosion by throwing
        // and rotating objects, available in some zone(radius).
        public void Explode()
        {
            if (_explosionVisualEffects != null)
            {
                Instantiate(_explosionVisualEffects, transform.position, Quaternion.identity);
            }
            
            // Get all objects that can be affected by explosion.
            Collider2D[] affectedColliders = GetObjectsCollidersInExplosionRadius();
            
            if (affectedColliders != null)
            {
                BreakAndDamageObjects(affectedColliders);
                
                // Update affectedColliders arrays in case
                // broken and damaged produced more objects.
                affectedColliders = GetObjectsCollidersInExplosionRadius();
                
                if (affectedColliders != null)
                {
                    ApplyPhysicsToObjects(affectedColliders);   
                }
            }
        }


        private void BreakAndDamageObjects(Collider2D[] affectedColliders)
        {
            // Apply damage to all object in explosion radius
            // that have Health or Destruction components.
            if (_damage != 0)
            {
                Destruction[] affectedDestructions = GetDestructionComponents(affectedColliders); 
                TrimArray(ref affectedDestructions);
                    
                if (affectedDestructions.Length != 0)
                {
                    BreakObjects(affectedDestructions);
                }
                else
                {
                    Health[] affectedHealths = GetHealthComponents(affectedColliders);
                    TrimArray(ref affectedHealths);
                        
                    if (affectedHealths.Length != 0)
                    {
                        DamageObjects(affectedHealths);
                    }
                }
            }
        }


        private void ApplyPhysicsToObjects(Collider2D[] affectedColliders)
        {
            // Get RigidBody2D components of that objects,
            // to be able to throw and add torque to objects.
            Rigidbody2D[] affectedRigidbody2Ds = GetRigidBody2DComponents(affectedColliders);
            TrimArray(ref affectedRigidbody2Ds);
                
            if (affectedRigidbody2Ds.Length != 0)
            {
                // Throw them in direction opposite to center
                // of explosion.
                ThrowObjects(affectedRigidbody2Ds);

                // Add torque to them.
                AddRandomTorque(affectedRigidbody2Ds);
            }
        }
        
        
        private void BreakObjects(Destruction[] affectedDestructions)
        {
            foreach (Destruction destruction in affectedDestructions)
            {
                destruction.Break(_damage);
            }
        }


        private void DamageObjects(Health[] affectedHealths)
        {
            foreach (Health health in affectedHealths)
            {
                health.TakeDamage(_damage);
            }
        }
        

        private Collider2D[] GetObjectsCollidersInExplosionRadius()
        {
            return Physics2D.OverlapCircleAll(_centerPoint.position, _radius, _affectedByExplosion);
        }
        
        
        private Rigidbody2D[] GetRigidBody2DComponents(Collider2D[] affectedColliders)
        {
            Rigidbody2D[] affectedRigidbody2Ds = new Rigidbody2D[affectedColliders.Length];
            int rigidBodiesIndex = 0;
            
            for (int collidersIndex = 0; collidersIndex < affectedColliders.Length; collidersIndex++)
            {
                Rigidbody2D rigidbody2D = null;
                
                rigidbody2D = affectedColliders[collidersIndex].gameObject.GetComponent<Rigidbody2D>();

                if (rigidbody2D != null)
                {
                    affectedRigidbody2Ds[rigidBodiesIndex] = rigidbody2D;
                    rigidBodiesIndex++;
                }
            }

            return affectedRigidbody2Ds;
        }


        private Destruction[] GetDestructionComponents(Collider2D[] affectedColliders)
        {
            Destruction[] affectedDestructions = new Destruction[affectedColliders.Length];
            int destructionsIndex = 0;

            for (int collidersIndex = 0; collidersIndex < affectedColliders.Length; collidersIndex++)
            {
                Destruction destruction = null;

                destruction = affectedColliders[collidersIndex].gameObject.GetComponent<Destruction>();

                if (destruction != null)
                {
                    affectedDestructions[destructionsIndex] = destruction;
                    destructionsIndex++;
                }
            }

            return affectedDestructions;
        }


        private Health[] GetHealthComponents(Collider2D[] affectedColliders)
        {
            Health[] affectedHealths = new Health[affectedColliders.Length];
            int healthIndex = 0;

            for (int collidersIndex = 0; collidersIndex < affectedColliders.Length; collidersIndex++)
            {
                Health health = null;

                health = affectedColliders[collidersIndex].gameObject.GetComponent<Health>();

                if (health != null)
                {
                    affectedHealths[healthIndex] = health;
                    healthIndex++;
                }
            }

            return affectedHealths;
        }


        // Note: Array that passed to that method
        // arranged in such way, that null elements
        // can be only in the end of the array(if null elements is exist).
        private void TrimArray(ref Rigidbody2D[] arrayToTrim)
        {
            int nonEmptyElemetnsCount = 0;

            for (int elementNumber = 0; elementNumber < arrayToTrim.Length; elementNumber++)
            {
                if (arrayToTrim[elementNumber] != null)
                {
                    nonEmptyElemetnsCount++;
                }
            }

            Array.Resize(ref arrayToTrim, nonEmptyElemetnsCount);
        }


        // Note: Array that passed to that method
        // arranged in such way, that null elements
        // can be only in the end of the array(if null elements is exist).
        private void TrimArray(ref Destruction[] arrayToTrim)
        {
            int nonEmptyElementsCount = 0;

            for (int index = 0; index < arrayToTrim.Length; index++)
            {
                if (arrayToTrim[index] != null)
                {
                    nonEmptyElementsCount++;
                }
            }

            Array.Resize(ref arrayToTrim, nonEmptyElementsCount);
        }
        
        
        // Note: Array that passed to that method
        // arranged in such way, that null elements
        // can be only in the end of the array(if null elements is exist).
        private void TrimArray(ref Health[] arrayToTrim)
        {
            int nonEmptyElementsCount = 0;

            for (int index = 0; index < arrayToTrim.Length; index++)
            {
                if (arrayToTrim[index] != null)
                {
                    nonEmptyElementsCount++;
                }
            }

            Array.Resize(ref arrayToTrim, nonEmptyElementsCount);
        }


        private void ThrowObjects(Rigidbody2D[] affectedRigidbody2Ds)
        {
            foreach (Rigidbody2D rigidBody in affectedRigidbody2Ds)
            {
                Vector2 forceDirection = (rigidBody.transform.position - _centerPoint.position).normalized;
                rigidBody.AddForce(forceDirection * _force, ForceMode2D.Impulse);
            }
        }


        private void AddRandomTorque(Rigidbody2D[] affectedRigidbody2Ds)
        {
            float torqueToAdd = Random.Range(_torqueLimits.minTorque, _torqueLimits.maxTorque);
            
            foreach (Rigidbody2D rigidBody in affectedRigidbody2Ds)
            {
                rigidBody.AddTorque(torqueToAdd);
            }
        }
    }
}