using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameBehaviour
{
    public class ScenesController : MonoBehaviour
    {
        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
