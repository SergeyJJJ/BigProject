using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public bool IsPressed { get; private set; } = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            IsPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsPressed = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsPressed = false;
        }
    }
}
