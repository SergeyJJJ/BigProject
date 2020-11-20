using System;
using Arsenal.WeaponsProjectiles.ProjectilesBehaviour;
using Arsenal.WeaponsProjectiles.ProjectilesData;
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

        [SerializeField] private float _force = 0;                            // Explosion force.
        [SerializeField] private float _radius = 0f;                          // Explosion radius.
        [SerializeField] private Transform _centerPoint = null;               // Explosion center point. Used as a point from which force will be applied.
        [SerializeField] private TorqueLimits _torqueLimits = default(TorqueLimits);   // Minimum and maximum torque of pieces.
        [SerializeField] private LayerMask _affectedByExplosion = Physics2D.AllLayers; // What can be affect by explosion.

        public void Initialize(LayerMask affectedByExplosion, float force = 0f, float radius = 0f,
                                        Transform centerPoint = null, TorqueLimits torqueLimits = default(TorqueLimits))
        {
            _affectedByExplosion = affectedByExplosion;
            _force = force;
            _radius = radius;
            _centerPoint = centerPoint;
            _torqueLimits = torqueLimits;
            _affectedByExplosion = affectedByExplosion;
        }


        public void Initialize(Rocket rocket)
        {
            _affectedByExplosion = rocket.HittableObjects;
            _force = rocket.ExplosionForce;
            _radius = rocket.ExplosionRadius;
            _centerPoint = rocket.ExplosionCenterPoint;
            _torqueLimits.maxTorque = rocket.ExplosionTorqueLimits.maxTorque;
            _torqueLimits.minTorque = rocket.ExplosionTorqueLimits.minTorque;
        }
        
        
        // Simulate an explosion by throwing
        // and rotating objects, available in some zone(radius).
        public void Explode()
        {
            // Get all objects that can be affected by explosion.
            Collider2D[] affectedColliders = GetObjectsCollidersInExplosionRadius();

            if (affectedColliders != null)
            {
                // Get RigidBody2D components of that objects,
                // to be able to throw and add torque to objects.
                Rigidbody2D[] affectedRigidbody2Ds = GetRigidBody2DComponents(affectedColliders);
                TrimRigidbody2DsArray(ref affectedRigidbody2Ds);
                
                // Throw them in direction opposite to center
                // of explosion.
                ThrowObjects(affectedRigidbody2Ds);

                // Add torque to them.
                AddRandomTorque(affectedRigidbody2Ds);
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


        // Note: Array that passed to that method
        // arranged in such way, that null elements
        // can be only in the end of the array(if null elements is exist).
        private void TrimRigidbody2DsArray(ref Rigidbody2D[] arrayToTrim)
        {
            int nonEmptyElemetnsCount = 0;

            for (int rigidBodyIndex = 0; rigidBodyIndex < arrayToTrim.Length; rigidBodyIndex++)
            {
                if (arrayToTrim[rigidBodyIndex] != null)
                {
                    nonEmptyElemetnsCount++;
                }
            }

            Array.Resize(ref arrayToTrim, nonEmptyElemetnsCount);
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