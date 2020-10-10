using UnityEngine;

namespace ForItemsAndCreatures
{
    public class Destroyer : MonoBehaviour
    {
        public void DestroyThisObject()
        {
            Destroy(gameObject);
        }


        public void DestroyParentObject()
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
