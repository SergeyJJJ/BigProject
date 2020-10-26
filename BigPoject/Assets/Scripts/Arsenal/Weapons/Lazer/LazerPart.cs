using GameBehaviour;
using UnityEngine;

namespace Arsenal.Weapons.Lazer
{
    public class LazerPart : MonoBehaviour
    {
        private void OnEnable()
        {
            EventSystem.TriggerEvent("OnLazerPartActive");
        }
    }
}
