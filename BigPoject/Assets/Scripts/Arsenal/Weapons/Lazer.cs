﻿using UnityEngine;

namespace Arsenal.Weapons
{
    [RequireComponent((typeof(SpriteRenderer)))]
    public class Lazer : Weapon
    {
        [SerializeField] private GameObject _lazerStart = null;                          // Start lazer gameObject that contains start lazer sprite.
        [SerializeField] private GameObject _lazerMiddle = null;                         // Middle lazer gameObject that contains middle lazer sprit.
        [SerializeField] private GameObject _lazerEnd = null;                            // End lazer gameObject that contains end lazer sprite.
        [SerializeField] private float _maxLazerLength = 1f;                             // Length of the lazer.
        [SerializeField] private LayerMask _damageableByLazer = Physics2D.AllLayers;     // Determine what can be damaged by lazer.
        
        private float _currentLazerLength = 1f;                                          // Current length of the lazer.
        
        private float _startSpriteWidth = 0f;                                            // Start lazer sprite length.
        private float _endSpriteWidth = 0f;                                              // End lazer sprite length.
        
        private SpriteRenderer _startSpriteRenderer = null;                              // SpriteRenderer component of the start lazer sparite.
        private SpriteRenderer _endSpriteRenderer = null;                                // SpriteRenderer component of the end lazer sparite.
        
        private bool _isShotTriggered = false;                                           // Check if player trigger shoot button.
        
        /* This is the minimum distance that must be
         between character and another object for that
         the lazer doesn`t go inside itself. 
         When that distances is already reached we turn off particular
         lazer object.*/
        float _middleMinDistance = 0.85f;                                                // When that distance is reached we turn off middle lazer.
        float _startMinDistance = 0.3f;                                                  // When that distance is reached we turn off start lazer.
        
        public override void AllowShoot(bool canShoot)
        {
            _isShotTriggered = canShoot;
        }


        private void Start()
        {
            _startSpriteRenderer = _lazerStart.gameObject.GetComponent<SpriteRenderer>();
            _endSpriteRenderer = _lazerEnd.gameObject.GetComponent<SpriteRenderer>();
            _currentLazerLength = _maxLazerLength;
        }
        

        private void Update()
        {
            if (_isShotTriggered)
            {
                CallShotEvent();
                
                RaycastHit2D ray = ThrowRaycast();
                CalculateLazerLength(ray);

                if (IsFarEnoughToObject(_startMinDistance))
                {
                    InitializeStartLazerPart();
                    ActivateLazerPart(_lazerStart);
                }

                if (IsFarEnoughToObject(_middleMinDistance))
                {
                    InitializeMiddlePart();
                    ActivateLazerPart(_lazerMiddle);
                }

                if (IsRayCollideSomething(ray))
                {
                    InitializeEndPart();
                    ActivateLazerPart(_lazerEnd);
                
                }
                else
                {
                    DeactivateLazerPart(_lazerEnd);
                }
            }
            else
            {
                DeactivateLazerPart(_lazerStart);
                DeactivateLazerPart(_lazerMiddle);
                DeactivateLazerPart(_lazerEnd);
                CallStopShotEvent();
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
            _lazerMiddle.transform.localScale = new Vector3((_currentLazerLength - _startSpriteWidth),
                                                            midleLocalScale.y,
                                                            midleLocalScale.z);
            _lazerMiddle.transform.localPosition = new Vector2((_currentLazerLength/2), 0f);
        }


        private void InitializeEndPart()
        {
            _endSpriteWidth = _endSpriteRenderer.bounds.size.x;
            _lazerEnd.transform.localPosition = new Vector2(_currentLazerLength - 0.5f, 0f);
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
            return Physics2D.Raycast(FirePoint.transform.position, transform.right, _maxLazerLength, _damageableByLazer);
        }


        private void CalculateLazerLength(RaycastHit2D ray)
        {
            if (IsRayCollideSomething(ray))
            {
                _currentLazerLength = Vector2.Distance(ray.point, FirePoint.transform.position);
            }
            else
            {
                _currentLazerLength = _maxLazerLength;
            }
        }
        
        
        private void CallShotEvent()
        {
            EventSystem.TriggerEvent("OnLazerShot");
        }


        private void CallStopShotEvent()
        {
            EventSystem.TriggerEvent("OnStopLazerShop");
        }


        private bool IsRayCollideSomething(RaycastHit2D ray)
        {
            return ray.collider != null;
        }


        private bool IsFarEnoughToObject(float distance)
        {
            return _currentLazerLength >= distance;
        }
    }
}
