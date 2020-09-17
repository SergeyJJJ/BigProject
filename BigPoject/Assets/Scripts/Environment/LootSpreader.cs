using System.Collections.Generic;
using System;
using UnityEngine;

namespace Environment
{
    public class LootSpreader : MonoBehaviour
    {
        [Serializable] public struct Loot                                         // Used as storage for different loot that will be thrown.
        {
            public int amount;
            public GameObject item;
        }
        [SerializeField] private Loot[] _loot = null;

        [SerializeField] private float _throwLootForce = 0f;                       // Force that will be applied to the loot to throw it in a difference direction.

        public void SpreadLoot()
        {
            for (int lootIndex = 0; lootIndex < _loot.Length; lootIndex++)
            {
                int itemAmount = _loot[lootIndex].amount;

                for (int itemIndex = 0; itemIndex < itemAmount; itemIndex++)
                {
                    GameObject thrownItem =
                        Instantiate(_loot[lootIndex].item, transform.position, Quaternion.identity);

                    Rigidbody2D thrownItemRigidbody2D = thrownItem.GetComponent<Rigidbody2D>();

                    if (thrownItemRigidbody2D != null)
                    {
                        thrownItemRigidbody2D.AddForce(GetRandomDirection() * _throwLootForce, ForceMode2D.Impulse);
                    }
                }
            }
        }


        private Vector2 GetRandomDirection()
        {
            int xMin = 5, xMax = 11;
            int yMin = 4, yMax = 8;
            
            int xDirection = UnityEngine.Random.Range(xMin, xMax) * RandomSign();
            int yDirection = UnityEngine.Random.Range(yMin, yMax) * RandomSign();

            return new Vector2(xDirection, yDirection).normalized;
        }

        private int RandomSign()
        {
            return UnityEngine.Random.Range(1, 3) == 1 ? -1 : 1;
        }
    }
}
