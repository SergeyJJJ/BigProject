using System.Collections;
using Environment.InterfacesOfUsing;
using UnityEngine;

namespace Environment.ThingsDestruction
{
    public class MiddleWeightDestruction : Destruction
    {
        [SerializeField] private ParticleSystem _hitParticles = null;           // Particles that used when object was hit.
        [SerializeField] private ParticleSystem _destructionParticles = null;   // Particles that used when object was destructed.
        [SerializeField] private ObjectSpreader _objects = null;                // Used to throw loot if its available.
        private Animator _animator = null;                                      // Animator component that used to play hit animation.                   

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        
        protected override void OnGetDamage()
        {
            if (_animator != null)
            {
                PlayHitAnimation();
            }

            if (_hitParticles != null)
            {
                SpawnHitParticles();
            }
        }


        protected override void OnDestruction()
        {
            Collider2D objectCollider = GetComponent<Collider2D>();
            if (objectCollider != null)
            {
                DisableGetDamageCollider(objectCollider);
            }

            if (_destructionParticles != null)
            {
                SpawnDestructionParticles();
            }
            
            if (_animator != null)
            {
                PlayCrushAnimation();
            }

            if (_objects != null)
            {
                ThrowLoot();
            }
        }


        private void ThrowLoot()
        {
            _objects.SpreadObjects();
        }


        private void DisableGetDamageCollider(Collider2D objectCollider)
        {
            objectCollider.enabled = false;
        }
        

        private void PlayHitAnimation()
        {
            _animator.SetTrigger("Hit");
        }
        
        
        private void PlayCrushAnimation()
        {
            _animator.SetTrigger("Crush");
        }
        
        
        private void SpawnHitParticles()
        {
            _hitParticles.Play();
        }


        private void SpawnDestructionParticles()
        {
            _destructionParticles.Play();
        }
    }
}
