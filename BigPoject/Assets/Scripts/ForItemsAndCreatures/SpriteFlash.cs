using System.Collections;
using UnityEngine;

namespace ForItemsAndCreatures
{
    public class SpriteFlash : MonoBehaviour {

        [SerializeField] private Color _flashColor = Color.clear;         // Color used for flash.
        [SerializeField] private float _flashDuration = 0f;               // How long flash will be going on.
        
        private Material _material;                                       // Material used in current object.

        private IEnumerator _flashCoroutine;                              // Contains flashCoroutine.

        public void Flash()
        {
            if (_flashCoroutine != null)
            {
                StopCoroutine(_flashCoroutine);
            }

            _flashCoroutine = DoFlashRoutine();
            StartCoroutine(_flashCoroutine);
        }
        
        
        private void Awake()
        {
            _material = GetComponent<SpriteRenderer>().material;
        }

    
        private void Start()
        {
            _material.SetColor("_FlashColor", _flashColor);
        }
        

        private IEnumerator DoFlashRoutine()
        {
            float lerpTime = 0;

            while (lerpTime < _flashDuration)
            {
                lerpTime += Time.deltaTime;
                float percentage = lerpTime / _flashDuration;

                SetFlashAmount(1f - percentage);
                yield return null;
            }
            SetFlashAmount(0);
        }
    
    
        private void SetFlashAmount(float flashAmount)
        {
            _material.SetFloat("_FlashAmount", flashAmount);
        }
    }
}