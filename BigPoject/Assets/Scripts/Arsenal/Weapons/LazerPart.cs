using UnityEngine;

namespace Arsenal.Weapons
{
    public class LazerPart : MonoBehaviour
    {
        private void OnEnable()
        {
            EventSystem.EventSystem.TriggerEvent("OnLazerPartActive");
        }
    }
}
