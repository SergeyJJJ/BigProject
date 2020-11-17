using Environment.ThingsDestruction;
using LivingBeings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arsenal
{
    public class ActiveBullet : MonoBehaviour
    {
        private LayerMask _hittableByBullet = Physics2D.AllLayers;                      // Determine what can be damaged by bullet.
        private Bullet _currentBullet = null;                                           // Used to get bullet data.
        private Vector2 _launchDirection = Vector2.zero;                                // Used to set direction in which bullet will be launched.
        private Vector2 _startFlightPosition = Vector2.zero;                            // Determine from which point bullet will be launched.
        private Rigidbody2D _rigidbody2D = null;                                        // Used to set launch direction.                            
        private SpriteRenderer _spriteRenderer = null;                                  // Used to set appropriate sprite to sprite renderer component of the bullet gameObject.
        private float _flightRange = 0;                                                 // Used to determine when bullet will be disabled.
        
        public void Initialize(Bullet bullet, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            _hittableByBullet = bullet.HittableObjects;
            _currentBullet = bullet;
            _launchDirection = new Vector2(launchDirection.x, launchDirection.y) * _currentBullet.FlightSpeed;
            transform.position = startLaunchPosition;
            _spriteRenderer.sprite = _currentBullet.FlightSprite;
            _flightRange = _currentBullet.FlightRange;
        }
        
        
        public void RandomizeLifeTimeInScatter(float scatter)
        {
            _flightRange += Random.Range(-scatter, scatter);
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

            if (flyedDistance > _flightRange)
            {
                DisableBullet();
            }
        }
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsObjectHittableByBullet(other.gameObject))
            {
                Destruction destruction = other.gameObject.GetComponent<Destruction>();
                if (destruction != null)
                {
                    destruction.Break(_currentBullet.Damage);
                }
                else
                {
                    Health health = other.gameObject.GetComponent<Health>();
                    if (health != null)
                    {
                        RaycastHit2D hitRay = new RaycastHit2D();     // Used to determine point in which bullet collide with object.
                        hitRay = Physics2D.Raycast(transform.position, transform.forward, _hittableByBullet);
                        health.TakeDamage(_currentBullet.Damage);
                    }
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
