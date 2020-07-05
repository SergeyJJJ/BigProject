using UnityEngine;

namespace Enemies.ChaseTypes
{
    public abstract class Chase : MonoBehaviour
    {
        public abstract void ChasePlayer(Transform player, Transform enemy, float chasingSpeed);
    }
}
