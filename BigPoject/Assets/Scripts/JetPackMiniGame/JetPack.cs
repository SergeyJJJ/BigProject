using UnityEngine;

namespace JetPackMiniGame
{
    public class JetPack : MonoBehaviour, ICollectable
    {
        [SerializeField] private GameObject _onPlayerVersion = null;       // GameObject that will be used when JetPack is on the player.

        [Space]
        [SerializeField] private GameObject _player = null;                // Suit to which JetPack will be attached. 

        public void Collect()
        {
            // Put JetPack on the player.
            PutOnThePlayer();    
        }


        private void PutOnThePlayer()
        {
            Vector2 jetPackPositionOnPlayer = new Vector2(0.05f, 0.9f);
            
            _onPlayerVersion.transform.SetParent(_player.transform);
            _onPlayerVersion.transform.localPosition = jetPackPositionOnPlayer;
            _onPlayerVersion.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
