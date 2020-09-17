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

        public void SpreadLoot()
        {
            // Pass through every element in Loot array, and instantiate every
            // item some amount of times. After item was instantiated apply 
            // force to it to throw it on some distance.
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
                        thrownItemRigidbody2D.AddForce(GetRandomDirection() * GetRandomThrowForce(), ForceMode2D.Impulse);
                    }
                }
            }
        }


        private Vector2 GetRandomDirection()
        {
            int xMin = 0, xMax = 4;
            int yDirection = 5;
            
            int xDirection = UnityEngine.Random.Range(xMin, xMax) * GetRandomSign();

            return new Vector2(xDirection, yDirection).normalized;
        }

        private int GetRandomSign()
        {
            return UnityEngine.Random.Range(1, 3) == 1 ? -1 : 1;
        }


        private int GetRandomThrowForce()
        {
            int minForce = 7, maxForce = 11;
            return UnityEngine.Random.Range(minForce, maxForce);
        }
    }
}
