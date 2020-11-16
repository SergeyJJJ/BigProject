using System;
using System.Collections.Generic;
using Environment.InterfacesOfUsing;
using ForItemsAndCreatures;
using UnityEngine;

namespace Environment.ThingsDestruction
{
    public class LightWeightDestruction : ThingsDestruction
    {
        [SerializeField] private SpriteFlash _spriteFlash = null;          // Used to do spriteFlash when entity is hitted.
        [SerializeField] private GameObject _objectParts = null;           // Broken object parts.
        [SerializeField] private Explosion _explosion = null;              // Used to broken object parts.  
        
        protected override void OnGetDamage()
        {
            if (_spriteFlash != null)
            {
                StartHitSpriteFlash();
            }
        }


        protected override void OnDestruction()
        {
            if (_objectParts != null && _explosion != null)
            {
                ThrowObjectParts();
            }
            
            Destroy(gameObject);
        }
        
        
        private void StartHitSpriteFlash()
        {
            _spriteFlash.Flash();
        }
        
        
        private void ThrowObjectParts()
        {
            _objectParts.transform.position = transform.position;
            _objectParts.SetActive(true);
            _explosion.Explode();
        }
    }
}
