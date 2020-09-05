using System;
using Environment.InterfacesOfUsing;
using UnityEngine;

namespace Environment
{
    public class Door : MonoBehaviour, IInteractable
    {
        private Animator _animator = null;                            // Doors gameObject animator component.
    
        public void Interact()
        {
            
        }


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        private void Open()
        {
            _animator.SetBool("IsDoorOpen", true);
        }


        private void Close()
        {
            _animator.SetBool("IsDoorOpen", false);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Open();
            }
        }
        
      
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Close();
            }
        }
    }
}