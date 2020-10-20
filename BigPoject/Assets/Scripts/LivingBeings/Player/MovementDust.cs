using GameBehaviour;
using UnityEngine;

namespace LivingBeings.Player
{
    public class MovementDust : MonoBehaviour
    {
        [SerializeField] private GameObject _jumpDust = null;                                                            // Store a reference to JumpDust gameObject;
        [SerializeField] private GameObject _hightLandDust = null;                                                       // Store a reference to HightLandDust gameObject;
        [SerializeField] private GameObject _middleLandDust = null;                                                      // Store a reference to MiddleLandDust gameObject;
        [SerializeField] private float _hightLandDustSpawnHeight = 0;                                                    // Minimum height to spawn a HeightLandDust.
        [SerializeField] private float _middleLandDustSpawnHeight = 0;                                                   // Minimum height to spawn a MiddleLandDust.
        [SerializeField] private CharacterMovement.MovementStateMachine.CharacterMovement _characterMovement = null;     // Reference to the CharacterMovement script. Used here to get LastFallHeight value.
        
        private void OnEnable()
        {
            EventSystem.StartListening("OnJump", SpawnJumpDust);
            EventSystem.StartListening("OnLand", SpawnLandDust);
        }
        

        private void SpawnLandDust()
        {
            if (_characterMovement.LastFallHeight > _hightLandDustSpawnHeight)
            {
                SpawnHightLandDust();
            }
            else if (_characterMovement.LastFallHeight > _middleLandDustSpawnHeight)
            {
                SpawnMiddleLandDust();
            }
        }


        private void SpawnHightLandDust()
        {
            Instantiate(_hightLandDust, transform.position, Quaternion.identity); 
        }
        
        
        private void SpawnMiddleLandDust()
        {
            Instantiate(_middleLandDust, transform.position, Quaternion.identity); 
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
