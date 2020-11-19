using Arsenal.WeaponsProjectiles.ProjectilesData;
using UnityEngine;

namespace Arsenal.WeaponsProjectiles.ProjectilesBehaviour
{
    public abstract class ActiveProjectile : MonoBehaviour
    {
        private LayerMask _hittableByProjectile = Physics2D.AllLayers;                      // Determine what can be damaged by bullet.
        private WeaponProjectile _currentProjectile = null;                                           // Used to get bullet data.
        private Vector2 _launchDirection = Vector2.zero;                                // Used to set direction in which bullet will be launched.
        private Vector2 _startFlightPosition = Vector2.zero;                            // Determine from which point bullet will be launched.
        private Rigidbody2D _rigidbody2D = null;                                        // Used to set launch direction.                            
        private SpriteRenderer _spriteRenderer = null;                                  // Used to set appropriate sprite to sprite renderer component of the bullet gameObject.
        private float _flightRange = 0;                                                 // Used to determine when bullet will be disabled.

        #region Properties

        /*

        protected Vector2 LaunchDirection
        {
            get => _launchDirection;
            set => _launchDirection = value;
        }

        protected Vector2 StartFlightPosition
        {
            get => _startFlightPosition;
            set => _startFlightPosition = value;
        }

        protected Rigidbody2D RigidbodyComponent => _rigidbody2D;

        protected SpriteRenderer SpriteRendererComponent => _spriteRenderer;
*/
        protected LayerMask HittableByProjectile => _hittableByProjectile;
        
        protected WeaponProjectile CurrentProjectile => _currentProjectile;
        
        protected float FlightRange
        {
            get => _flightRange;
            set => _flightRange = value;
        }

        #endregion Properties

        public void Initialize(WeaponProjectile projectile, Vector2 launchDirection, Vector2 startLaunchPosition)
        {
            _currentProjectile = projectile;
            _hittableByProjectile = projectile.HittableObjects;
            transform.position = startLaunchPosition;
            _launchDirection = new Vector2(launchDirection.x, launchDirection.y) * _currentProjectile.FlightSpeed;
            _flightRange = _currentProjectile.FlightRange;
            _spriteRenderer.sprite = _currentProjectile.FlightSprite;
        }


        protected abstract void OnTriggerEnter2D(Collider2D other);


        protected void DisableBullet()
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
            Debug.Log("Awake form AcvtiveProjectile class");
        }
        
        
        private void Update()
        {
            float flownDistance = Mathf.Abs(transform.position.x - _startFlightPosition.x);

            if (flownDistance > _flightRange)
            {
                Debug.Log("Update form AcvtiveProjectile class");
                DisableBullet();
            }
        }
    }
}
