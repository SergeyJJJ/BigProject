using Assets.Scripts.Arsenal.Bullets;
using UnityEngine;

namespace Assets.Scripts.Arsenal.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private string _name = "";                 // Name of the weapon.
        [SerializeField] private string _description = "";          // Description of the weapon.
        [SerializeField] private Sprite _inGameSprite = null;       // Sprite that will be used for displaying in character`s hand.
        [SerializeField] private Bullet _bulletType = null;         // Contains information about bullet that current weapon will use.
        [SerializeField] private int _maxBulletsAmount = 0;         // Maximum bullets amount.
        [SerializeField] private float _fireRate = 0f;              // Weapon fire rate. 

        #region Protperties
        
        public string Name => _name;

        public string Description => _description;

        public Sprite InGameSprite => _inGameSprite;

        public Bullet BulletType => _bulletType;
        
        public int BulletsAmount => _maxBulletsAmount;

        public float FireRate => _fireRate;
        
        #endregion Properties
        
        public abstract void Shoot();
    }
}
