using System;
using Environment.InterfacesOfUsing;
using UnityEngine;

namespace UI
{
    // Class that controls Interact Button behaviour
    public class InteractButton : MonoBehaviour
    {
        private CustomButton _interactButton = null;          // Component that used to check if button was pressed.
        private Animator _animator = null;                    // Component that used to play button animations.
        private IInteractable _objectToInteract = null;       // Object with which player can interact now if interaction button is pressed. 
        private bool _isCanInteract = false;                  // Determine if player can interact with something by pressing interaction button.
        private bool _isButtonWasPressed = false;             // Determine if interact button was pressed.
        
        #region Properties

        public IInteractable ObjectToInteract
        {
            private get => _objectToInteract;
            set => _objectToInteract = value;
        }
        
        #endregion Properties
        
        public void MakeButtonUsable()
        {
            _animator.SetBool("IsButtonAppeared", true);
            _isCanInteract = true;
        }


        public void MakeButtonUnusable()
        {
            _isCanInteract = false;
            _objectToInteract = null;
            _animator.SetBool("IsButtonAppeared", false);
        }


        private void Awake()
        {
            _interactButton = GetComponent<CustomButton>();
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            // If Player can interact with something,
            // allow user interact with it by pressing 
            // interaction button.
            if (_isCanInteract)
            {
                if (_interactButton.IsPressed)
                {
                    if (!_isButtonWasPressed)
                    {
                        _isButtonWasPressed = true;
                        _objectToInteract?.Interact();
                    }
                }
                else
                {
                    _isButtonWasPressed = false;
                }
            }
        }
    }
}
