using System.Collections;
using Environment.InterfacesOfUsing;
using UnityEngine;

namespace Environment
{
    public class Crystal : MonoBehaviour, IBreakable
    {
        [SerializeField] private int _strength = 0;                            // How many times crystal can be hit before it will be broken. 
        [SerializeField] private Sprite _defaultSprite = null;                 // Default sprite of crystal.
        [SerializeField] private Sprite _hittableSprite = null;                // Sprite used when player hit crystal.
        private SpriteRenderer _spriteRenderer = null;                         // SpriteRender component of the crystal gameObject.                

        public void Break()
        {
            _strength--;
            StartCoroutine(FlickRoutine());
            
            if (_strength <= 0)
            {
                Destroy();
            }
        }


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        

        private void Destroy()
        {
            Destroy(gameObject);
        }


        private IEnumerator FlickRoutine()
        {
            float spriteChangeDelay = 0.035f; 
            
            _spriteRenderer.sprite = _hittableSprite;
            
            yield return new WaitForSeconds(spriteChangeDelay);

            _spriteRenderer.sprite = _defaultSprite;
        }
    }
}
