using System;
using System.Collections;
using System.Collections.Generic;
using ForItemsAndCreatures;
using UnityEngine;

namespace LivingBeings
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth = 0;                                // Maximum health amount that have entity.

        [Header("Visual Effects")]
        [SerializeField] private ParticleSystem _hitBlood = null;                     // Particles that used to represent hit blood.
        [SerializeField] private ParticleSystem _deathBlood = null;                     // Particles that used to represent death blood. 
        [SerializeField] private Animator _animator = null;                           // Animator that used to play hit animation.
        [SerializeField] private SpriteFlash _spriteFlash = null;                     // Used to do spriteFlash when entity is hitted.
        [SerializeField] private Explosion _explosion = null;                         // Used to throw body parts of the entity.
        [SerializeField] private GameObject _deathBodyParts = null;                   // Dead body parts.
        [SerializeField] private Vector2 _shiftFixForDeathBodySpawn = Vector2.zero;   // Used to spawn body parts in proper position. 
        
        [Header("Spawn Points")]
        [SerializeField] private Transform _deathBodyPartsSpawnPoint = null;          // Where dead body parts will be spawned.
        [SerializeField] private Transform _bloodParticlesSpawnPoint = null;          // Where blood particles will be spawned.
        
        private float _currentHealth = 0;                                             // Current health amount that have entity.
        private bool _isCanBeDamaged = true;                                          // Define if entity can be damaged.


        #region Properties

        public float MaxHealth => _maxHealth;

        public float CurrentHealth
        {
            get => _currentHealth;
            
            set
            {
                if (value < 0)
                {
                    _currentHealth = 0;
                }
                else if (value > _maxHealth)
                {
                    _currentHealth = _maxHealth;
                }
                else
                {
                    _currentHealth = value;
                }
            }
        }

        #endregion Properties

        public void TakeDamage(float damageAmount)
        {
            if (_isCanBeDamaged == true)
            {
                if (damageAmount < 0)
                {
                    damageAmount = -damageAmount;
                }

                CurrentHealth -= damageAmount;

                if (CurrentHealth > 0)
                {
                    OnGetDamage();
                }
                else
                {
                    _isCanBeDamaged = false;
                    OnDeath();
                }
            }
        }


        public void TakeTreatment(float hpAmount)
        {
            if (hpAmount < 0)
            {
                hpAmount = -hpAmount;
            }

            CurrentHealth += hpAmount;
        }


        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }
        
        
        private void OnGetDamage()
        {
            if (_hitBlood != null)
            {
                SpawnHitBloodParticles();
            }

            if (_spriteFlash != null)
            {
                StartHitSpriteFlash();
            }
        }


        private void OnDeath()
        {
            Rigidbody2D entityRigidbody2D = GetComponent<Rigidbody2D>();

            if (entityRigidbody2D != null)
            {
                StopMovement(entityRigidbody2D);
            }
            
            if (_animator != null)
            {
                PlayDeathAnimation();
            }

            if (_deathBlood != null)
            {
                SpawnDeathBloodParticles();
            }

            if (_deathBodyParts != null && _explosion != null)
            {
                StartCoroutine(ThrowBodyPartsRoutine());
            }
        }


        private void SpawnHitBloodParticles()
        {
            if (_bloodParticlesSpawnPoint)
            {
                _hitBlood.transform.position = _bloodParticlesSpawnPoint.position;
            }
            
            _hitBlood.Play();
        }

        
        private void SpawnDeathBloodParticles()
        {
            if (_bloodParticlesSpawnPoint)
            {
                _deathBlood.transform.position = _bloodParticlesSpawnPoint.position;
            }
            
            _deathBlood.Play();
        }
        
        

        private void PlayDeathAnimation()
        {
            _animator.SetTrigger("Death");
        }
        

        private void StopMovement(Rigidbody2D entityRigidbody2D)
        {
            entityRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        
        
        private void StartHitSpriteFlash()
        {
            _spriteFlash.Flash();
        }


        private IEnumerator ThrowBodyPartsRoutine()
        {
            yield return new WaitForSeconds(0.11f);
            
            Vector2 shiftedDeathBodyPartsPosition = new Vector2(_deathBodyPartsSpawnPoint.position.x + _shiftFixForDeathBodySpawn.x,
                                                                _deathBodyPartsSpawnPoint.position.y + _shiftFixForDeathBodySpawn.y);

            _deathBodyParts.transform.position = shiftedDeathBodyPartsPosition;
            _deathBodyParts.SetActive(true);
            _explosion.Explode();
        }


        private void Awake()
        {
            _currentHealth = _maxHealth;
        }
    }
}
