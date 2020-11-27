using UnityEngine;

namespace Arsenal.WeaponsProjectiles.ProjectilesData
{ 
    /*
    * Can be important to know.
    * 
    * this class is a parent class for Rocket and Bullet classes.
    * If you check them you can see that Bullet class does not have
    * any data, and Rocket class have only one field of data.
    * So, if Bullet and Rocket share almost the same data, why dont we
    * make Rocket inherit from Bullet?
    * That is because firstly Rocket it is not actually a bullet, and
    * secondly if you check ActiveProjectile, ActiveBullet and ActiveRocket hierarchy,
    * you can see that that classes are in the same relationship, so for ease of use
    * all of this classes,
    * we made Projectile, Bullet and Rocket classes in such relationship as
    * previous three classes.
    */
    
    public class Projectile : ScriptableObject
    {
        [SerializeField] private float _damage = 0f;
        [SerializeField] private float _flightRange = 0f;
        [SerializeField] private float _flightSpeed = 0f;
        [SerializeField] private Sprite _flightSprite = null;
        [SerializeField] private LayerMask _hittableObjets = Physics2D.AllLayers; 

        #region Properties

        public float Damage => _damage;

        public float FlightRange => _flightRange;

        public float FlightSpeed => _flightSpeed;

        public Sprite FlightSprite => _flightSprite;

        public LayerMask HittableObjects => _hittableObjets;

        #endregion    
    }
}
