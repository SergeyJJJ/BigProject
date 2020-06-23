using UnityEngine;

namespace Assets.Scripts.Arsenal.Weapons
{
    [RequireComponent((typeof(SpriteRenderer)))]
    public class Icegun : Weapon
    {
        public override void AllowShoot(bool canShoot)
        {
            throw new System.NotImplementedException();
        }
    }
}
