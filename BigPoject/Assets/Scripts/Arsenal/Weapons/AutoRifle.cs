using Assets.Scripts.Arsenal.Bullets;
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
            GameObject bullet = null;
            bullet = GetBullet();
            
            InitializeBullet(bullet);
            
            LaunchBullet(bullet);

            DecrementBulletsCount();
        }


        private GameObject GetBullet()
        {
            GameObject bullet = null;
            bullet = BulletPool.SharedInstance.GetBullet();
            return bullet;
        }


        private void InitializeBullet(GameObject bullet)
        {
            ActiveBullet activeBullet = bullet.GetComponent<ActiveBullet>();
            activeBullet.CurrentBullet = BulletType;
        }


        private void LaunchBullet(GameObject bullet)
        {
            bullet.transform.position = FirePoint.position;
            bullet.SetActive(true);
        }
    }
}
