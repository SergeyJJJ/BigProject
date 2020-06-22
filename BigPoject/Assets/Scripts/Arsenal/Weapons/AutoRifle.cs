using UnityEngine;

namespace Assets.Scripts.Arsenal.Weapons
{
    [RequireComponent((typeof(SpriteRenderer)))]
    public class AutoRifle : Weapon
    {
        private SpriteRenderer _spriteRenderer = null;
        
        private void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }


        private void Start()
        {
            _spriteRenderer.sprite = InGameSprite;
        }
        
        
        public override void Shoot()
        {
            throw new System.NotImplementedException();
        }
    }
}
