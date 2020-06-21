using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private Weapon _currentWeapon = null;
        private SpriteRenderer _spriteRenderer = null;

        private void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
    }
}