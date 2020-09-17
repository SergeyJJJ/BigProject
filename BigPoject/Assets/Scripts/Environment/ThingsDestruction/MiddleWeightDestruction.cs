using System.Collections;
using Environment.InterfacesOfUsing;
using UnityEngine;

namespace Environment.ThingsDestruction
{
    public class MiddleWeightDestruction : MonoBehaviour, IBreakable
    {
        [SerializeField] private int _strength = 0; // How many times crystal can be hit before it will be broken.
        [SerializeField] private ParticleSystem _hitParticles = null; // Particles that used when object was hit.
        [SerializeField] private LootSpreader _loot = null; // Used to throw loot if its available.
        private Animator _animator = null; // Animator component that used to play hit animation.                   

        public void Break()
        {
            _strength--;

            if (_strength > 0)
            {
                OnGetDamage();
            }
            else
            {
                OnDestruction();
            }
        }


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        private void OnGetDamage()
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


        private void OnDestruction()
        {
            Collider2D objectCollider = GetComponent<Collider2D>();
            if (objectCollider != null)
            {
                DisableGetDamageCollider(objectCollider);
            }

            if (_animator != null)
            {
                PlayCrushAnimation();
            }

            if (_loot != null)
            {
                ThrowLoot();
            }
        }


        private void Destroy()
        {
            Destroy(gameObject);
        }


        private void ThrowLoot()
        {
            _loot.SpreadLoot();
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
    }
}
