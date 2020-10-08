using System.Collections.Specialized;
using Environment.InterfacesOfUsing;
using UI;
using UnityEngine;

namespace LivingBeings.Player
{
    public class InteractionZone : MonoBehaviour
    {
        [SerializeField] private InteractButton _interactButton = null;       // Button that used to interact with something. From InteractionZone class we assign with what interaction will be performed. 
        private OrderedDictionary _interactables = new OrderedDictionary();   // Contains all interactable objects which player triggered at this moment.  
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();

            if (interactable != null)
            {
                _interactables.Add(other, interactable);
                _interactButton.MakeButtonUsable();
                _interactButton.ObjectToInteract = interactable;
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (_interactables.Contains(other))
            {
                _interactables.Remove(other);

                if (_interactables.Count != 0)
                {
                    IInteractable interactable = (IInteractable)_interactables[_interactables.Count - 1];
                    _interactButton.ObjectToInteract = interactable;
                }
                else
                {
                    _interactButton.MakeButtonUnusable();
                }
            }
        }
    }
}