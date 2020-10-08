using UnityEngine;

namespace ForItemsAndCreatures
{
    public class Destroyer : MonoBehaviour
    {
        public void DestroyThisObject()
        {
            Destroy(gameObject);
        }
    }
}
