using System;
using UnityEngine;

namespace Living_beings.Enemies
{
    public class EnemyAnimations : MonoBehaviour
    {
        private Animator _animator = null;       // Used to control enemy`s animations.
        
        public void StartIdleAnimation()
        {
            _animator.SetBool("IsPatrolling", false);
        }


        public void StartWalkingAnimation()
        {
            _animator.SetBool("IsPatrolling", true);
        }
        
        
        private void Awake()
        {  
            _animator = gameObject.GetComponent<Animator>();
        }
    }
}
