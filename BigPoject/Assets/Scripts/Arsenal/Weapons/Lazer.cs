﻿using UnityEngine;

namespace Arsenal.Weapons
{
    [RequireComponent((typeof(SpriteRenderer)))]
    public class Lazer : Weapon
    {
        [SerializeField] private GameObject _lazerStart = null;      // Start lazer gameObject that contains start lazer sprite.
        [SerializeField] private GameObject _lazerMiddle = null;     // Middle lazer gameObject that contains middle lazer sprit.
        [SerializeField] private GameObject _lazerEnd = null;        // End lazer gameObject that contains end lazer sprite.
        [SerializeField] private float _maxLazerLength = 1f;         // Length of the lazer.
        
        private float _currentLazerLength = 1f;                      // Current length of the lazer.
        
        private float _startSpriteWidth = 0f;                        // Start lazer sprite length.
        private float _endSpriteWidth = 0f;                          // End lazer sprite length.
        
        private SpriteRenderer _startSpriteRenderer = null;          // SpriteRenderer component of the start lazer sparite.
        private SpriteRenderer _endSpriteRenderer = null;            // SpriteRenderer component of the end lazer sparite.
        
        private bool _isShotTriggered = false;                       // Check if player trigger shoot button.
        
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
            Debug.DrawLine(FirePoint.transform.position, new Vector3(FirePoint.transform.position.x + _currentLazerLength, FirePoint.transform.position.y, FirePoint.transform.position.z), Color.red, 0f);
            if (_isShotTriggered)
            {
                if (!IsLazerPartActiated(_lazerStart))
                {
                    InitializeStartLazerPart();
                    ActivateLazerPart(_lazerStart);
                }
                
                InitializeMiddlePart();
                ActivateLazerPart(_lazerMiddle);
                    
                RaycastHit2D ray = ThrowRaycast();
                if (IsRayCollideSomething(ray))
                {
                    OnRayCollision(ray);
                    
                    if (!IsLazerPartActiated(_lazerEnd))
                    {
                        InitializeEndPart();
                        ActivateLazerPart(_lazerEnd);
                    }
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
            }
        }
        

        private void InitializeStartLazerPart()
        {
            _startSpriteWidth = _startSpriteRenderer.bounds.size.x;
            _lazerStart.transform.position = FirePoint.transform.position;
        }


        private void InitializeMiddlePart()
        {
            Vector3 midleLocalScale = _lazerMiddle.transform.localScale;
            _lazerMiddle.transform.localScale = new Vector3(2 * (_currentLazerLength - _startSpriteWidth),
                                                            midleLocalScale.y,
                                                            midleLocalScale.z);
            Debug.Log(_currentLazerLength);
            _lazerMiddle.transform.localPosition = new Vector2((_currentLazerLength/2), 0f);
        }


        private void InitializeEndPart()
        {
            _endSpriteWidth = _endSpriteRenderer.bounds.size.x;
            _lazerEnd.transform.position = new Vector2(_currentLazerLength, 0f);
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
            return Physics2D.Raycast(FirePoint.transform.position, transform.right, _maxLazerLength);
        }


        private void OnRayCollision(RaycastHit2D ray)
        { 
            // -- Get the laser length
            _currentLazerLength = Vector2.Distance(ray.point, FirePoint.transform.position);
            ActivateLazerPart(_lazerEnd);
        }
        

        private bool IsLazerPartActiated(GameObject lazerPart)
        {
            return lazerPart.activeInHierarchy;
        }


        private bool IsRayCollideSomething(RaycastHit2D ray)
        {
            return ray.collider != null;
        }
    }
}
