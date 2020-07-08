using Creatures.Player.CharacterMovement.MovementStateMachine;
using UnityEngine;

namespace DevelopmentProcessHelpers
{
    public class FallSaver : MonoBehaviour
    {
        [SerializeField] private Transform _respawnPoint = null;        // Point to which character will be moved if he fall out from the screen.


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                CharacterMovement characterMovement = other.gameObject.GetComponent<CharacterMovement>();
                characterMovement.TranslateTo(_respawnPoint.position);
            }
        }
    }
}
