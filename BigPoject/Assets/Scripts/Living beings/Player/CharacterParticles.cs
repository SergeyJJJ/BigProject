using GameBehaviour;
using UnityEngine;

namespace Living_beings.Player
{
    public class CharacterParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _runParticles = null;                // Particles that used when character is running.

        private void OnEnable()
        {
            // Subscribe to characters events.
            EventSystem.StartListening("OnRun", StartRunParticles);
            
            EventSystem.StartListening("OnStop", StopRunParticles);
        }


        private void StartRunParticles()
        {
            _runParticles.Play();
        }


        private void StopRunParticles()
        {
            _runParticles.Stop();
        }


        private void OnDisable()
        {
            // Unsubscribe to characters events.
            EventSystem.StopListening("OnRun", StartRunParticles);
            
            EventSystem.StopListening("OnStop", StopRunParticles);
        }
    }
}
