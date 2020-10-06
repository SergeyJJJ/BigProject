using System;
using UnityEngine;

namespace Environment
{
    public class ObjectSpreader : MonoBehaviour
    {
        [Serializable] public struct Object                                         // Used as storage for different loot that will be thrown.
        {
            public int amount;
            public GameObject item;
        }
        [SerializeField] private Object[] _objects = null;

        public void SpreadObjects()
        {
            // Pass through every element in Objects array, and instantiate every
            // item some amount of times. After item was instantiated apply 
            // force to it to throw it on some distance.
            for (int lootIndex = 0; lootIndex < _objects.Length; lootIndex++)
            {
                int itemAmount = _objects[lootIndex].amount;

                for (int itemIndex = 0; itemIndex < itemAmount; itemIndex++)
                {
                    GameObject thrownItem =
                        Instantiate(_objects[lootIndex].item, transform.position, Quaternion.identity);

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
