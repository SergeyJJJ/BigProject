﻿using LivingBeings;
using UnityEngine;

namespace Environment.JetPackObstaclesBehaviour
{
    public class LazerObstacle : MonoBehaviour
    {
        [SerializeField] private float _damage = 0f;                                     // How many damage lazer will apply.
        [Space]
        [SerializeField] private bool _isLazerPersistent = false;                        // Determine if lazer works without stops.
        [SerializeField] private float _delayBeforeFirstShot = 0f;                       // How much time must pass before first shot.
        [SerializeField] private float _shootingTime = 0f;                               // How long lazer can shoot per one time.
        [SerializeField] private float _restingTime = 0f;                                // How long lazer rest after each shoot.
        [Space]
        [SerializeField] private GameObject _firePoint = null;                           // Point from which lazer is shooting.
        [SerializeField] private GameObject _lazerStart = null;                          // Start lazer gameObject that contains start lazer sprite.
        [SerializeField] private GameObject _lazerMiddle = null;                         // Middle lazer gameObject that contains middle lazer sprit.
        [SerializeField] private GameObject _lazerEnd = null;                            // End lazer gameObject that contains end lazer sprite.
        [Space]
        [SerializeField] private float _maxLazerLength = 1f;                             // Length of the lazer.
        [SerializeField] private LayerMask _interactabelWithLazer = Physics2D.AllLayers; // Determine what can be damaged by lazer.
        [SerializeField] private LayerMask _hittableByLazer = Physics2D.AllLayers;       // Determine what can be damaged by bullet.
        [Space]
        [SerializeField] private float _middleLazerScaleFixer = 0f;                      // Used to provide seamless connection between middle lazer and start lazer parts. Need when rotating a lazer.            
        
        private bool _isLazerActive = false;                                             // Check if lazer is active now.
        private float _shootTimer = 0f;                                                  // Timer that control how long lazer will shot.
        private float _restTimer = 0f;                                                   // Timer that control how long lazer will rest after each shoot.
        private float _firstShotDelayTimer = 0;                                          // Timer that control how long lazer will wait before performing shot for the first time.
        private float _currentLazerLength = 1f;                                          // Current length of the lazer.
        private float _startSpriteWidth = 0f;                                            // Start lazer sprite length.
        private SpriteRenderer _startSpriteRenderer = null;                              // SpriteRenderer component of the start lazer sparite.

        /*
           This is the minimum distance that must be
           between character and another object for that
           the lazer doesn`t go inside itself. 
           When that distances is already reached we
           turn off particular lazer object.
        */
        private float _middleMinDistance = 0.85f;                                        // When that distance is reached we turn off middle lazer.
        private float _startMinDistance = 0.55f;                                         // When that distance is reached we turn off start lazer.
        

        private void Start()
        {
            _startSpriteRenderer = _lazerStart.gameObject.GetComponent<SpriteRenderer>();
            _currentLazerLength = _maxLazerLength;
            
            // Initialize timers.
            _firstShotDelayTimer = _delayBeforeFirstShot;
            _shootTimer = _shootingTime;
        }


        private void Update()
        {
            // If time before first shoot has passed.
            if (_firstShotDelayTimer <= 0)
            {
                // If rest time has passed, now we can shoot.
                if (_restTimer <= 0)
                {
                    // If shoot time hasn`t passed we still shooting.
                    if (_shootTimer >= 0)
                    {
                        if (!_isLazerPersistent)
                        {
                            _shootTimer -= Time.deltaTime;
                        }

                        _isLazerActive = true;
                        
                        RaycastHit2D ray = ThrowRaycast();
                        CalculateLazerLength(ray);

                        if (IsFarEnoughToObject(_startMinDistance))
                        {
                            InitializeStartLazerPart();
                            ActivateLazerPart(_lazerStart);
                        }
                        else
                        {
                            DeactivateLazerPart(_lazerStart);
                        }

                        if (IsFarEnoughToObject(_middleMinDistance))
                        {
                            InitializeMiddlePart();
                            ActivateLazerPart(_lazerMiddle);
                        }
                        else
                        {
                            DeactivateLazerPart(_lazerMiddle);
                        }

                        if (IsRayCollideSomething(ray))
                        {
                            if (IsRayCanApplyDamageTo(ray.collider.gameObject))
                            {
                                ApplyDamageTo(ray.collider.gameObject, ray.point);
                            }
                            InitializeEndPart();
                            ActivateLazerPart(_lazerEnd);
                        }
                        else
                        {
                            DeactivateLazerPart(_lazerEnd);
                        }
                    }
                    // If shoot time has passed reset shoot timer and rest timer.
                    else
                    {
                        _restTimer = _restingTime;       
                    }
                }
                // If lazer still have a rest decrease rest timer.
                else
                {
                    if (_isLazerActive)
                    {
                        _isLazerActive = false;
                        _shootTimer = _shootingTime;
                        
                        DeactivateLazerPart(_lazerStart);
                        DeactivateLazerPart(_lazerMiddle);
                        DeactivateLazerPart(_lazerEnd);
                    }
                    
                    _restTimer -= Time.deltaTime;
                }
            }
            // If lazer didn`t perform it`s first shoot decrease delay timer.
            else
            {
                _firstShotDelayTimer -= Time.deltaTime;
            }
        }


        private void InitializeStartLazerPart()
        {
            _startSpriteWidth = _startSpriteRenderer.bounds.size.x;
            _lazerStart.transform.localPosition = Vector2.zero;
        }


        private void InitializeMiddlePart()
        {
            Vector3 midleLocalScale = _lazerMiddle.transform.localScale;
            _lazerMiddle.transform.localScale = new Vector3((_currentLazerLength - _startSpriteWidth) + _middleLazerScaleFixer,
                midleLocalScale.y,
                midleLocalScale.z);
            _lazerMiddle.transform.localPosition = new Vector2((_currentLazerLength/2), 0f);
        }


        private void InitializeEndPart()
        {
            float offset = 0.5f;           // This offset is needed for the end sprite is placed in properly position.
            _lazerEnd.transform.localPosition = new Vector2(_currentLazerLength - offset, 0f);
        }


        private void ActivateLazerPart(GameObject lazerPart)
        {
            lazerPart.SetActive(true);   
        }


        private void DeactivateLazerPart(GameObject lazerPart)
        {
            lazerPart.SetActive(false);  
        }


        private RaycastHit2D ThrowRaycast()
        {
            return Physics2D.Raycast(_firePoint.transform.position, transform.right, _maxLazerLength, _interactabelWithLazer);
        }


        private void CalculateLazerLength(RaycastHit2D ray)
        {
            if (IsRayCollideSomething(ray))
            {
                _currentLazerLength = Vector2.Distance(ray.point, _firePoint.transform.position);
            }
            else
            {
                _currentLazerLength = _maxLazerLength;
            }
        }
        
        
        private void ApplyDamageTo(GameObject collidedObject, Vector2 hitPosition)
        {
            bool isComponentExist = false;
            isComponentExist = collidedObject.TryGetComponent<Health>(out var health);
            
            if (isComponentExist)
            {
                health.TakeDamage(_damage);
            }
        }
        

        private bool IsRayCollideSomething(RaycastHit2D ray)
        {
            return ray.collider != null;
        }


        private bool IsFarEnoughToObject(float distance)
        {
            return _currentLazerLength >= distance;
        }


        private bool IsRayCanApplyDamageTo(GameObject collidedObject)
        {
            return ((1 << collidedObject.gameObject.layer) & _hittableByLazer) != 0;
        }
    }
}
