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

        private void OnEnable()
        {
            EventSystem.StartListening("OnLazerActivation", ThrowEnergyConcentrationParticles);
            EventSystem.StartListening("OnLazerShot", StopEnergyConcentrationParticles);
            EventSystem.StartListening("OnLazerShot", ThrowEnergyFlowingParticles);
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

                    flowingParticlesShape.scale = new Vector3((_lazer.CurrentLazerLength - _lazer.StartSpriteWidth),
                        flowingParticlesShape.scale.y,
                        flowingParticlesShape.scale.z);
                    
                    _energyFlowingParticles.transform.localPosition = new Vector2(_lazer.CurrentLazerLength/2, _lazer.FirePoint.transform.localPosition.y);

                    _energyFlowingParticles.gameObject.SetActive(true);
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
                    _energyFlowingParticles.gameObject.SetActive(false);
                }
            }
        }


        private void OnDisable()
        {
            EventSystem.StopListening("OnLazerActivation", ThrowEnergyConcentrationParticles);
            EventSystem.StopListening("OnLazerShot", StopEnergyConcentrationParticles);
            EventSystem.StopListening("OnLazerShot", ThrowEnergyFlowingParticles);
            EventSystem.StopListening("OnLazerDeactivation", StopEnergyConcentrationParticles);
        }
    }
}
