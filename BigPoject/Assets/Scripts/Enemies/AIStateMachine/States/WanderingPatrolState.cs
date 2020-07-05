using UnityEngine;

namespace Enemies.AIStateMachine.States
{
    public class WanderingPatrolState : PatrollingState
    {
        private Vector2 _currentTargetPoint = Vector2.zero;                  // Current target point.
        private int _currentPointIndex = 0;                                  // Target point index.
        
        public WanderingPatrolState(EnemyAI enemyAI, EnemyStateMachine enemyStateMachine) : base(enemyAI, enemyStateMachine)
        {
            _currentTargetPoint = _enemyAI.PatrolTrajectoryPoints[0].position;
        }
        
        
        public override void Enter()
        {
            base.Enter();
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (IsPlayerDetected())
            {
                _enemyStateMachine.TransitionToState(_enemyAI.Chasing);
            }
            
            // If list is containing at least one point.
            if (IsPointsExist())
            {
                // Move enemy to the target point.
                Move();

                // If platform reached the target point.
                if (IsReachedPoint())
                {
                    // Change current target point.
                    ChangeTargetPoint();
                    _enemyStateMachine.TransitionToState(_enemyAI.Standing);
                }
            }
        }


        public override void Exit()
        {
            base.Exit();
        }
        
        
        private void Move()
        {
            float step = _enemyAI.WanderingSpeed * Time.deltaTime;
            Vector2 desiredPosition = Vector2.MoveTowards(_enemyAI.TransformComponent.position, _currentTargetPoint, step);

            // Move platform to the desired position.
            _enemyAI.TransformComponent.position = desiredPosition;
        }
        
        
        private void ChangeTargetPoint()
        {
            _currentTargetPoint = GetNextPoint();
        }
        
        
        private Vector2 GetNextPoint()
        {
            // Here will be stored point to be returned.
            Vector2 nextPoint = Vector2.zero;

            // If current point index is equal to points amount,
            // reset point index to zero and set first point to return.
            if (_currentPointIndex == _enemyAI.PatrolTrajectoryPoints.Count - 1)
            {
                _currentPointIndex = 0;
                nextPoint = _enemyAI.PatrolTrajectoryPoints[_currentPointIndex].position;
            }
            // If it is not, than increment point index and set next point to return.
            else
            {
                nextPoint = _enemyAI.PatrolTrajectoryPoints[_currentPointIndex + 1].position;
                _currentPointIndex++;
            }

            // return index.
            return nextPoint;
        }
        
        
        private bool IsPointsExist()
        {
            return _enemyAI.PatrolTrajectoryPoints != null;
        }


        private bool IsReachedPoint()
        {
            float threshold = 0.1f;

            // If distance between platform and target point is less than allowable threshold
            // than return that the platform reached the goal.
            return Vector2.Distance(_enemyAI.TransformComponent.position, _currentTargetPoint) < threshold;
        }
    }
}
