using UnityEngine;

namespace Assets.Scripts.Arsenal.Bullets
{
    public class ActiveBullet : MonoBehaviour
    {
        private Bullet _currentBullet = null;
        private Rigidbody2D _rigidbody2D = null;
        private SpriteRenderer _spriteRenderer = null;

        #region Properties

        public Bullet CurrentBullet
        {
            get => _currentBullet;
            set => _currentBullet = value;
        }

        #endregion

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        
        private void Start()
        {
            _spriteRenderer.sprite = _currentBullet.FlightSprite;

            // Launch bullet.
            Vector2 flightVelocity = new Vector2(_currentBullet.FlightSpeed, 0);
            _rigidbody2D.velocity = flightVelocity;
        }
        
        
        private void OnTriggerEnter2D()
        {
            DisableBullet();
        }

        
        private void DisableBullet()
        {
            gameObject.SetActive(false);
        }
    }
}
