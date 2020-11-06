using System;
using GameBehaviour;
using UnityEngine;

namespace Arsenal.Weapons.Lazer
{
    public class LazerParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _energyConcentrationParticles = null;   // Particles that used to represent energy concentration effect.
        [SerializeField] private ParticleSystem _energyFlowingParticles = null;         // Particles that used to represent particles flowing along tha lazer.
        [SerializeField] private Lazer _lazer = null;                                   // Used to get lazer characteristics.
        [SerializeField] private GameObject _forwardFlowingParticlesDestroyer = null;         // Destroys flowing particles when them touch to it.
        
        private void OnEnable()
        {
            EventSystem.StartListening("OnLazerActivation", ThrowEnergyConcentrationParticles);
            EventSystem.StartListening("OnLazerShot", StopEnergyConcentrationParticles);
            EventSystem.StartListening("OnLazerShot", ReplaceFlowingParticlesDestroyerToLazerEnd);
            EventSystem.StartListening("OnLazerShot", ThrowEnergyFlowingParticles);
            EventSystem.StartListening("OnLazerDeactivation", StopEnergyFlowingParticles);
            EventSystem.StartListening("OnLazerDeactivation", StopEnergyConcentrationParticles);
        }


        private void ThrowEnergyConcentrationParticles()
        {
            if (_energyConcentrationParticles != null)
            {
                if (!_energyConcentrationParticles.isPlaying)
                {
                    _energyConcentrationParticles.transform.position = _lazer.FirePoint.transform.position;
                    _energyConcentrationParticles.Play();
                }
            }
        }


        private void ThrowEnergyFlowingParticles()
        {
            if (_energyFlowingParticles != null)
            {
                if (!_energyFlowingParticles.isPlaying)
                {
                    var flowingParticlesShape = _energyFlowingParticles.shape;
                    
                    _energyFlowingParticles.Play();
                }
            }
        }


        private void StopEnergyConcentrationParticles()
        {
            if (_energyConcentrationParticles != null)
            {
                if (_energyConcentrationParticles.isPlaying)
                {
                    _energyConcentrationParticles.Stop();
                }
            }
        }
        

        private void StopEnergyFlowingParticles()
        {
            if (_energyFlowingParticles != null)
            {
                if (_energyFlowingParticles.isPlaying)
                {
                    _energyFlowingParticles.Stop();
                }
            }
        }


        private void ReplaceFlowingParticlesDestroyerToLazerEnd()
        {
            Vector2 replacePosition = new Vector2(_lazer.CurrentLazerLength, _forwardFlowingParticlesDestroyer.transform.localPosition.y);
            _forwardFlowingParticlesDestroyer.transform.localPosition = replacePosition;
        }


        private void OnDisable()
        {
            EventSystem.StopListening("OnLazerActivation", ThrowEnergyConcentrationParticles);
            EventSystem.StopListening("OnLazerShot", StopEnergyConcentrationParticles);
            EventSystem.StopListening("OnLazerShot", ThrowEnergyFlowingParticles);
            EventSystem.StopListening("OnLazerDeactivation", StopEnergyFlowingParticles);
            EventSystem.StopListening("OnLazerDeactivation", StopEnergyConcentrationParticles);
        }
    }
}
