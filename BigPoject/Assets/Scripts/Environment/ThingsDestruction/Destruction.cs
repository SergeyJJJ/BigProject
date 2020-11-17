using UnityEngine;

namespace Environment.ThingsDestruction
{
    public abstract class Destruction : MonoBehaviour
    {
        [SerializeField] private float _safetyMargin = 0;                       // How many healthPoint have an object.
        
        public void Break(float damageAmount)
        {
            _safetyMargin -= damageAmount;

            if (_safetyMargin > 0)
            {
                OnGetDamage();
            }
            else
            {
                OnDestruction();
            }
        }


        protected abstract void OnGetDamage();


        protected abstract void OnDestruction();
    }
}
