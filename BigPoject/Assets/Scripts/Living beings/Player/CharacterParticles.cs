using GameBehaviour;
using UnityEngine;

namespace Living_beings.Player
{
    public class CharacterParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _runParticles = null;                // Particles that used when character is running.
        [SerializeField] private ParticleSystem _landParticles = null;               // Particles that used when character is landing.

        private void OnEnable()
        {
            // Subscribe to characters events.
            EventSystem.StartListening("OnRun", StartRunParticles);
            EventSystem.StartListening("OnStop", StopRunParticles);

            EventSystem.StartListening("OnLand", StartLandParticles);
        }


        private void StartRunParticles()
        {
            _runParticles.Play();
        }


        private void StopRunParticles()
        {
            _runParticles.Stop();
        }


        private void StartLandParticles()
        {
            _landParticles.Play();
        }


        private void OnDisable()
        {
            // Unsubscribe to characters events.
            EventSystem.StopListening("OnRun", StartRunParticles);
            EventSystem.StopListening("OnStop", StopRunParticles);
            
            EventSystem.StopListening("OnLand", StartLandParticles);
        }
    }
}
