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
        [SerializeField] private ParticleSystem _blood = null;                        // Particles that used to represent blood. 
        [SerializeField] private Animator _animator = null;                           // Animator that used to play hit animation.
        [SerializeField] private SpriteRenderer _renderer = null;                     // Used to work with entities sprites. 
        [SerializeField] private Explosion _explosion = null;                         // Used to throw body parts of the entity.
        [SerializeField] private GameObject _deathBodyParts = null;                   // Dead body parts.
        [SerializeField] private Transform _deathBodyPartsSpawnPoint = null;          // Where dead body parts will be spawned.
        [SerializeField] private Vector2 _shiftFixForDeathBodySpawn = Vector2.zero;   // Used to spawn body parts in proper position. 
        
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
            if (_blood != null)
            {
                SpawnBloodParticles();
            }

            if (_renderer != null)
            {
                StartCoroutine(ChangeSpriteToHitColorRoutine());
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

            if (_deathBodyParts != null && _explosion != null)
            {
                StartCoroutine(ThrowBodyPartsRotine());
            }
        }


        private void SpawnBloodParticles()
        {
            _blood.Play();
        }


        private void PlayDeathAnimation()
        {
            _animator.SetTrigger("Death");
        }


        /*private void PlayHitAnimation()
        {
            _animator.SetTrigger("Hit");
        }*/ // Obsolete.

        
        private IEnumerator ChangeSpriteToHitColorRoutine()
        {
            _renderer.color = Color.red;

            float timeInHitColorTime = 0.18f;
            yield return new WaitForSeconds(timeInHitColorTime);
            
            _renderer.color = Color.white;
        }
        

        private void StopMovement(Rigidbody2D entityRigidbody2D)
        {
            entityRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }


        private IEnumerator ThrowBodyPartsRotine()
        {
            yield return new WaitForSeconds(0.1f);
            
            _deathBodyParts.transform.position = new Vector2(_deathBodyPartsSpawnPoint.position.x - 2, _deathBodyPartsSpawnPoint.position.y + 0.5f);
            _deathBodyParts.SetActive(true);
            _explosion.Explode();
        }


        private void Awake()
        {
            _currentHealth = _maxHealth;
        }
    }
}
