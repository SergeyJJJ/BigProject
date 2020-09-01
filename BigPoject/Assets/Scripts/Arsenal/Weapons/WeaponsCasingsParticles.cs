using GameBehaviour;
using UnityEngine;

namespace Arsenal.Weapons
{
    public class WeaponsCasingsParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _rifleCasings = null;             // Particles that used to represent AutoRifle casings.


        private void OnEnable()
        {
            EventSystem.StartListening("OnRifleShot", ThrowRifleCasings);
        }


        private void ThrowRifleCasings()
        {
            if (_rifleCasings != null)
            {
                _rifleCasings.Play();
            }
        }


        private void OnDisable()
        {
            EventSystem.StopListening("OnRifleShot", ThrowRifleCasings);
        }
    }
}
