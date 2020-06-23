using System.Runtime.InteropServices.WindowsRuntime;
using Assets.Scripts.Arsenal.Bullets;
using UnityEngine;

namespace Assets.Scripts.Arsenal.Weapons
{
    [RequireComponent((typeof(SpriteRenderer)))]
    public class AutoRifle : Weapon
    {
        private SpriteRenderer _spriteRenderer = null;

        public override void Shoot()
        {
            GameObject bullet = GetBullet();

            Vector2 launchDirection = GetLaunchDirection();

            InitializeBullet(bullet, launchDirection);
            
            LaunchBullet(bullet);

            DecrementBulletsCount();
        }

        
        private void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }


        private void Start()
        {
            _spriteRenderer.sprite = InGameSprite;
        }
        

        private GameObject GetBullet()
        {
            GameObject bullet = null;
            bullet = BulletPool.SharedInstance.GetBullet();
            return bullet;
        }


        private void InitializeBullet(GameObject bullet, Vector2 launchDirection)
        {
            ActiveBullet activeBullet = bullet.GetComponent<ActiveBullet>();
            activeBullet.Initialize(BulletType, launchDirection);
        }


        private void LaunchBullet(GameObject bullet)
        {
            bullet.transform.position = FirePoint.position;
            bullet.SetActive(true);
        }


        private Vector2 GetLaunchDirection()
        {
            return Vector2.right;
        }
    }
}
