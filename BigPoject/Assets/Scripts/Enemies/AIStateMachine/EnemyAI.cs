using System;
using System.Collections.Generic;
using Enemies.AIStateMachine.States;
using Enemies.ChaseTypes;
using UnityEngine;

namespace Enemies.AIStateMachine
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("Patrolling state")]
        [SerializeField] private bool _isAlwaysStanding = false;                  // Defines if enemy is always standing at one place.
        [SerializeField] private float _wanderingSpeed = 0f;                      // Enemy`s speed when wandering.
        [SerializeField] private float _standingDuration = 0f;                    // How long enemy will stay before moving to the next point.
        [SerializeField] private List<Transform> _wanderTrajectoryPoints = null;  // Contains path points along which the enemy moves.
        
        [Space]
        [Header("Chasing state")]
        [SerializeField] private Chase _chaseAction = null;                       // Type of chasing that use current enemy.
        [SerializeField] private float _chasingSpeed = 0f;                        // Chasing speed.
        
        [Space]
        [Header("Help components")]
        [SerializeField] private PlayerDetector _playerDetector = null;           // Component that detect if player is near the enemy.
        [SerializeField] private PlatformEndDetector _platformEndDetector = null; // Component that detect end of platform.
        [SerializeField] private GameObject _player = null;                       // Reference to the player.
        
        private Transform _transform = null;                                      // Enemy`s transform component.
        
        // States
        private AttackingState _attackingState = null;
        private ChasingState _chasingState = null;
        private WanderingPatrolState _wanderingPatrolState = null;
        private StandingPatrolState _standingPatrolState = null;

        #region Properties

        private EnemyStateMachine _enemyStateMachine = null;

        public bool IsAlwaysStanding => _isAlwaysStanding;

        public List<Transform> PatrolTrajectoryPoints => _wanderTrajectoryPoints;

        public float WanderingSpeed => _wanderingSpeed;

        public float StandingDuration => _standingDuration;

        public Chase ChaseAction => _chaseAction;

        public float ChasingSpeed => _chasingSpeed;

        public PlayerDetector PlayerCheck => _playerDetector;

        public PlatformEndDetector PlaformEndCheck => _platformEndDetector;

        public Transform TransformComponent => transform;
        
        public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;

        public AttackingState Attacking => _attackingState;

        public ChasingState Chasing => _chasingState;

        public StandingPatrolState Standing => _standingPatrolState;

        public WanderingPatrolState Wandering => _wanderingPatrolState;

        public GameObject Player => _player;

        #endregion
        
        
        private void Awake()
        {
            _transform = gameObject.transform;
            
            _enemyStateMachine = new EnemyStateMachine();
            _attackingState = new AttackingState(this, _enemyStateMachine);
            _chasingState = new ChasingState(this, _enemyStateMachine);
            _standingPatrolState = new StandingPatrolState(this, _enemyStateMachine);
            _wanderingPatrolState = new WanderingPatrolState(this, _enemyStateMachine);
        }
        
        
        private void Start()
        {
            if (_isAlwaysStanding)
            {
                _enemyStateMachine.Initialization(_standingPatrolState);
            }
            else
            {
                _enemyStateMachine.Initialization(_wanderingPatrolState);
            }
        }


        private void FixedUpdate()
        {
            _enemyStateMachine.CurrentState.PhysicsUpdate();
        }
    }
}
