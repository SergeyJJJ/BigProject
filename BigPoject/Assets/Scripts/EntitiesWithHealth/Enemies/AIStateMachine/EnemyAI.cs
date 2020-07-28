﻿using EntitiesWithHealth.Enemies.AIStateMachine.States;
using EntitiesWithHealth.Enemies.AttackTypes;
using EntitiesWithHealth.Enemies.ChaseTypes;
using EntitiesWithHealth.Enemies.PatrolTypes;
using UnityEngine;

namespace EntitiesWithHealth.Enemies.AIStateMachine
{
    // Class that processes together all enemy`s behaviours.
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private bool _isAlwaysStanding = false;                  // Check if enemy is always standing.
        
        [Space]
        [Header("Patrolling state control")]
        [SerializeField] private Patrol _patrolType = null;                       // Type of patrolling that use current enemy.
        
        [Space]
        [Header("Chasing state control")]
        [SerializeField] private Chase _chaseType = null;                         // Type of chasing that use current enemy.

        [Space]
        [Header("Attacking state control")] [SerializeField]
        private Attack _attackType = null;                                        // Type of attack that use current enemy.

        [Space]
        [Header("Help components")]
        [SerializeField] private ChaseZoneDetector _chaseZoneDetector = null;     // Component that detect if player is near the enemy.
        [SerializeField] private AttackZoneDetector _attackZoneDetector = null;   // Component that detect if player is in attack zone.
        [SerializeField] private GameObject _player = null;                       // Reference to the player.

        private Rigidbody2D _rigidbody = null;                                    // Player`s rigidbody2D component.
        private Transform _transform = null;                                      // Enemy`s transform component.
        
        // States
        private AttackingState _attackingState = null;
        private ChasingState _chasingState = null;
        private PatrollingState _patrollingState = null;
        private EnemyStateMachine _enemyStateMachine = null;
        
        #region Properties

        public bool IsAlwaysStanding => _isAlwaysStanding;

        public Patrol EnemyPatrol => _patrolType;
        
        public Chase EnemyChase => _chaseType;

        public Attack EnemyAttack => _attackType;
        
        public ChaseZoneDetector ChaseDetector => _chaseZoneDetector;

        public AttackZoneDetector AttackDetector => _attackZoneDetector;

        public Rigidbody2D RigidbodyComponent => _rigidbody;

        public Transform TransformComponent => _transform;
        
        public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;

        public AttackingState Attacking => _attackingState;

        public ChasingState Chasing => _chasingState;

        public PatrollingState Patrolling => _patrollingState;

        public GameObject Player => _player;

        #endregion
        
        
        private void Awake()
        {
            _transform = gameObject.transform;

            _rigidbody = gameObject.GetComponent<Rigidbody2D>();
            
            _enemyStateMachine = new EnemyStateMachine();
            _attackingState = new AttackingState(this, _enemyStateMachine);
            _chasingState = new ChasingState(this, _enemyStateMachine);
            _patrollingState = new PatrollingState(this, _enemyStateMachine);
        }
        
        
        private void Start()
        {
            _enemyStateMachine.Initialization(_patrollingState);
        }


        private void FixedUpdate()
        {
            _enemyStateMachine.CurrentState.PhysicsUpdate();
        }
    }
}
