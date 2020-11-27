using System;
using Arsenal.WeaponsProjectiles.ProjectilesData;
using UnityEngine;

namespace Arsenal.WeaponsProjectiles.ProjectilesBehaviour
{
    public abstract class ActiveProjectile : MonoBehaviour
    {
        private LayerMask _hittableByProjectile = Physics2D.AllLayers;                  // Determine what can be damaged by bullet.
        private Vector2 _launchDirection = Vector2.zero;                                // Used to set direction in which bullet will be launched.
        private Vector2 _startFlightPosition = Vector2.zero;                            // Determine from which point bullet will be launched.
        private Rigidbody2D _rigidbody2D = null;                                        // Used to set launch direction.                            
        private SpriteRenderer _spriteRenderer = null;                                  // Used to set appropriate sprite to sprite renderer component of the bullet gameObject.
        private float _flightRange = 0;                                                 // Used to determine when bullet will be disabled.

        #region Properties
        
        protected LayerMask HittableByProjectile => _hittableByProjectile;
        
        protected float FlightRange
        {
            get => _flightRange;
            set => _flightRange = value;
        }

        #endregion Properties

        public void Initialize(Projectile projectile, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            _hittableByProjectile = projectile.HittableObjects;
            transform.position = startLaunchPosition;
            _startFlightPosition = startLaunchPosition;
            _launchDirection = new Vector2(launchDirection.x, launchDirection.y) * projectile.FlightSpeed;
            _flightRange = projectile.FlightRange;
            _spriteRenderer.sprite = projectile.FlightSprite;
        }


        public virtual void InitializeRocket(Rocket rocket, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            
        }


        public virtual void InitializeBullet(Bullet bullet, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            
        }


        protected abstract void OnTriggerEnter2D(Collider2D other);


        protected void DisableProjectile()
        {
            gameObject.SetActive(false);
        }
        
        
        protected bool IsObjectHittableByProjectile(GameObject hitObject)
        {
            return ((1 << hitObject.layer) & _hittableByProjectile) != 0;
        }
        
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void OnEnable()
        {
            _rigidbody2D.velocity = _launchDirection;
        }


        private void Update()
        {
            float flownDistance = Mathf.Abs(transform.position.x - _startFlightPosition.x);

            if (flownDistance > _flightRange)
            {
                DisableProjectile();
            }
        }
    }
}
