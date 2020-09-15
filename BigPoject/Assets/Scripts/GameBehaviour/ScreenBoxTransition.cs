using Living_beings.Player.CharacterMovement.MovementStateMachine;
using UnityEngine;

namespace GameBehaviour
{
    public class ScreenBoxTransition : MonoBehaviour
    {
        [SerializeField] private GameObject _firstFrame = null;            // First frame from which we can transit to another frames.
        [SerializeField] private GameObject _secondFrame = null;           // Second frame from which we can transit to another frames.
        [SerializeField] private Transform _leftPoint = null;              // Point in which direction  will be moved character if he will go to the left screenBox.
        [SerializeField] private Transform _rightPoint = null;             // Point to which direction will be moved character if he will go to the left screenBox.
        private CharacterMovement _characterMovement = null;               // Characters movement control script.


        private void Start()
        {
            _characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        }
    
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Vector2 characterNextFramePosition = Vector2.zero;
                
                if (_firstFrame.activeInHierarchy)
                {
                    _firstFrame.SetActive(false);
                    _secondFrame.SetActive(true);
                    characterNextFramePosition = _rightPoint.position;
                }
                else if (_secondFrame.activeInHierarchy)
                {
                    _secondFrame.SetActive(false);
                    _firstFrame.SetActive(true);
                    characterNextFramePosition = _leftPoint.position;
                }

                if (characterNextFramePosition != Vector2.zero)
                {
                    _characterMovement.TranslateTo(characterNextFramePosition);
                }
            }
        }
    }
}
