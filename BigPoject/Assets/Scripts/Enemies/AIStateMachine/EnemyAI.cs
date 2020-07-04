using System;
using System.Collections.Generic;
using Enemies.AIStateMachine.States;
using UnityEngine;

namespace Enemies.AIStateMachine
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private bool _isAlwaysStanding = false;                 // Defines if enemy is always standing at one place.
        [SerializeField] private List<Transform> _wanderTrajectoryPoints = null; // Contains path points along which the enemy moves.
        [SerializeField] private float _wanderingSpeed = 0f;                     // Enemy`s speed when wandering.
        [SerializeField] private float _standingDuration = 0f;                   // How long enemy will stay before moving to the next point.

        private Transform _transform = null;                                     // Enemy`s transform component.
        
        // States

        private AttackingState _attackingState = null;
        private ChasingState _chasingState = null;
        private WanderingState _wanderingState = null;
        private StandingState _standingState = null;

        #region Properties

        private EnemyStateMachine _enemyStateMachine = null;

        public bool IsAlwaysStanding => _isAlwaysStanding;

        public List<Transform> PatrolTrajectoryPoints => _wanderTrajectoryPoints;

        public float WanderingSpeed => _wanderingSpeed;

        public float StandingDuration => _standingDuration;

        public Transform TransformComponent => transform;
        
        public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;

        public AttackingState Attacking => _attackingState;

        public ChasingState Chasing => _chasingState;

        public WanderingState Wandering => _wanderingState;

        public StandingState Standing => _standingState;

        #endregion
        
        
        private void Awake()
        {
            _transform = gameObject.transform;
            
            _enemyStateMachine = new EnemyStateMachine();
            _attackingState = new AttackingState(this, _enemyStateMachine);
            _chasingState = new ChasingState(this, _enemyStateMachine);
            _wanderingState = new WanderingState(this, _enemyStateMachine);
            _standingState = new StandingState(this, _enemyStateMachine);
        }
        
        
        private void Start()
        {
            // Set first state.
            if (_isAlwaysStanding)
            {
                _enemyStateMachine.Initialization(_standingState);
            }
            else
            {
                _enemyStateMachine.Initialization(_wanderingState);
            }
        }


        private void FixedUpdate()
        {
            _enemyStateMachine.CurrentState.PhysicsUpdate();
        }
    }
}
