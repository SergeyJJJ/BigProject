using Enemies.AIStateMachine.States;
using Enemies.ChaseTypes;
using Enemies.PatrolTypes;
using UnityEngine;

namespace Enemies.AIStateMachine
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private bool _isAlwaysStanding = false;                  // Check if enemy is always standing.
        
        [SerializeField] private Patrol _patrolType = null;                       // Type of patrolling that use current enemy.
        
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
        private PatrollingState _patrollingState = null;
        private EnemyStateMachine _enemyStateMachine = null;
        
        #region Properties

        public bool IsAlwaysStanding => _isAlwaysStanding;

        public Patrol EnemyPatrol => _patrolType;
        
        public Chase ChaseAction => _chaseAction;

        public float ChasingSpeed => _chasingSpeed;

        public PlayerDetector PlayerCheck => _playerDetector;

        public PlatformEndDetector PlaformEndCheck => _platformEndDetector;

        public Transform TransformComponent => transform;
        
        public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;

        public AttackingState Attacking => _attackingState;

        public ChasingState Chasing => _chasingState;

        public PatrollingState Patrolling => _patrollingState;

        public GameObject Player => _player;

        #endregion
        
        
        private void Awake()
        {
            _transform = gameObject.transform;
            
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
