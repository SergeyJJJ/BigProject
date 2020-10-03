using System;
using GameBehaviour;
using UnityEngine;

namespace Living_beings.Player
{
    public class MovementDust : MonoBehaviour
    {
        [SerializeField] private GameObject _jumpDust = null;      // Store a reference to JumpDust gameObject;
        [SerializeField] private GameObject _landDust = null;      // Store a reference to LandDust gameObject;

        private void OnEnable()
        {
            EventSystem.StartListening("OnJump", SpawnJumpDust);
            EventSystem.StartListening("OnLand", SpawnLandDust);
        }
        

        private void SpawnLandDust()
        {
            Instantiate(_landDust, transform.position, Quaternion.identity);    
        }


        private void SpawnJumpDust()
        {
            Instantiate(_jumpDust, transform.position, Quaternion.identity);
        }
        
        
        private void OnDisable()
        {
            EventSystem.StopListening("OnJump", SpawnJumpDust);
            EventSystem.StopListening("OnLand", SpawnLandDust);
        }
    }
}
