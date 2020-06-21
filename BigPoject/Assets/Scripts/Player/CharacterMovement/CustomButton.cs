using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Player.CharacterMovement
{
    public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        private bool _isPressed = false;
        public bool IsPressed
        {
            get
            {
                return _isPressed;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isPressed = false;
        }
    }
}
