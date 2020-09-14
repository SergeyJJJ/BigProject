using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    public class LootSpreader : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _lootStorage = null;             // Storage for loot that must be spread.
        [SerializeField] private float _throwLootForce = 0f;                       // Force that will be applied to the loot to throw it in a difference direction.

        public void SpreadLoot()
        {
            foreach (GameObject loot in _lootStorage)
            {
                Rigidbody2D lootRigidbody2D = loot.GetComponent<Rigidbody2D>();

                if (lootRigidbody2D != null)
                {
                    lootRigidbody2D.AddForce(GetRandomDirection() * _throwLootForce, ForceMode2D.Impulse);
                }
            }
        }
        
        
        private Vector2 GetRandomDirection()
        {
            int xMin = 5, xMax = 11;
            int yMin = 4, yMax = 8;
            
            int xDirection = Random.Range(xMin, xMax) * RandomSign();
            int yDirection = Random.Range(yMin, yMax) * RandomSign();

            return new Vector2(xDirection, yDirection).normalized;
        }

        private int RandomSign()
        {
            return Random.Range(1, 3) == 1 ? -1 : 1;
        }
    }
}
