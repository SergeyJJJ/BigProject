using UnityEngine;

namespace Assets.Scripts.Arsenal.Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ActiveWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon _currentWeapon = null;
        private SpriteRenderer _spriteRenderer = null;
        private Sprite _sprite = null;

        private void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }


        private void Start()
        {
            Initialize(_currentWeapon);
            _spriteRenderer.sprite = _sprite;
        }


        public void Shoot()
        {
            _currentWeapon.Shoot();
        }
        

        private void Initialize(Weapon weapon)
        {
            _sprite = _currentWeapon.InGameSprite;
        }
    }
}