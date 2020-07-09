﻿using Destructables;
using UnityEngine;

namespace Arsenal.Bullets
{
    public class ActiveBullet : MonoBehaviour
    {
        [SerializeField] private LayerMask _hittableByBullet = Physics2D.AllLayers;     // Determine what can be damaged by bullet.
        private Bullet _currentBullet = null;
        private Vector2 _launchDirection = Vector2.zero;
        private Vector2 _startFlightPosition = Vector2.zero;
        private Rigidbody2D _rigidbody2D = null;
        private SpriteRenderer _spriteRenderer = null;
        
        public void Initialize(Bullet bullet, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            _currentBullet = bullet;
            _launchDirection = new Vector2(launchDirection.x * _currentBullet.FlightSpeed, launchDirection.y);
            transform.position = startLaunchPosition;
            _spriteRenderer.sprite = _currentBullet.FlightSprite;
        }
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void OnEnable()
        {
            _startFlightPosition = transform.position;
            _rigidbody2D.velocity = _launchDirection;
        }


        private void Update()
        {
            float flyedDistance = Mathf.Abs(transform.position.x - _startFlightPosition.x);

            if (flyedDistance > _currentBullet.FlightRange)
            {
                DisableBullet();
            }
        }
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            bool isObjectHittableByBullet = ((1 << other.gameObject.layer) & _hittableByBullet) != 0;
            
            if (isObjectHittableByBullet)
            {
                ApplyDamageTo(other.gameObject);
                DisableBullet();
            }
        }

        
        private void DisableBullet()
        {
            gameObject.SetActive(false);
        }
        
        
        private void ApplyDamageTo(GameObject damageable)
        {
            bool isComponentExist = false;
            isComponentExist = damageable.TryGetComponent<Health>(out var health);
            
            if (isComponentExist)
            {
                health.TakeDamage(_currentBullet.Damage);
            }
        }
    }
}
