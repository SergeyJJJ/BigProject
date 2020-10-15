﻿using System;
using Environment.InterfacesOfUsing;
using LivingBeings;
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
            if (IsObjectHittableByBullet(other.gameObject))
            {
                IBreakable breakable = other.gameObject.GetComponent<IBreakable>();
                if (breakable != null)
                {
                    breakable.Break();
                }

                Health health = other.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    RaycastHit2D hitRay = new RaycastHit2D();     // Used to determine point in which bullet collide with object.
                    hitRay = Physics2D.Raycast(transform.position, transform.forward, _hittableByBullet);
                    health.TakeHurt(_currentBullet.Damage, hitRay.point);
                }

                DisableBullet();
            }
        }

        
        private void DisableBullet()
        {
            gameObject.SetActive(false);
        }
        

        private bool IsObjectHittableByBullet(GameObject hitObject)
        {
            return ((1 << hitObject.layer) & _hittableByBullet) != 0;
        }
    } 
}
