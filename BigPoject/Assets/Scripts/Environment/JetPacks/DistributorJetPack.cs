﻿using UnityEngine;

namespace Environment.JetPacks
{
    public class DistributorJetPack : MonoBehaviour, ICollectable
    {
        [SerializeField] private GameObject _blueSuit = null;                   // Used to make appropriate JetPack child of it.
        [SerializeField] private GameObject _jetPackBlueSuit = null;            // Used to make appropriate JetPack child of it.
        [SerializeField] private GameObject _flyingJetPack = null;              // JetPack that used in JetPackMiniGame. It will be attached to _jetPackBlueSuit.
        [SerializeField] private GameObject _doubleJumpJetPack = null;          // JetPack that used to perform doubleJump. It will be attached to _blueSuit. 
        
        public void Collect()
        {
            DistributeJetPacks();
        }


        private void DistributeJetPacks()
        {
            Vector2 jetPackPositionOnSuit = new Vector2(0.05f, 0.9f);
            
            MakeJetPackAsAChild(_flyingJetPack.transform, _jetPackBlueSuit.transform);
            SetJetPackPositionOnSuit(_flyingJetPack.transform, jetPackPositionOnSuit);
            ActivateJetPack(_flyingJetPack);
            
            MakeJetPackAsAChild(_doubleJumpJetPack.transform, _blueSuit.transform);
            SetJetPackPositionOnSuit(_doubleJumpJetPack.transform, jetPackPositionOnSuit);
            ActivateJetPack(_doubleJumpJetPack);
            
            Destroy(gameObject);
        }


        private void MakeJetPackAsAChild(Transform jetPack, Transform parentGameObject)
        {
            jetPack.SetParent(parentGameObject);
        }


        private void ActivateJetPack(GameObject jetPack)
        {
            jetPack.SetActive(true);
        }


        private void SetJetPackPositionOnSuit(Transform jetPack, Vector2 positionOnSuit)
        {
            jetPack.localPosition = positionOnSuit;
        }
    }
}