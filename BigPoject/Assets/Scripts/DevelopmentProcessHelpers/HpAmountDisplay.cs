using LivingBeings;
using TMPro;
using UnityEngine;

namespace DevelopmentProcessHelpers
{
    public class HpAmountDisplay : MonoBehaviour
    {
        [SerializeField] private Health _healthComponent = null;        // Health component form which we will take current health amount.
        [SerializeField] private TextMeshPro _healhtText = null;        // Text that will show health above the creature in game.

        private void Update()
        {
            _healhtText.text = _healthComponent.CurrentHealth < 1 ? $"Died" : _healthComponent.CurrentHealth.ToString();
        }
    }
}