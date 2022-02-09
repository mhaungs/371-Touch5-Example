using UnityEngine;

namespace Agents.Patroller
{
    public class PatrollerLostState : PatrollerBaseState
    {
        public PatrollerLostState(Transform target, Transform eye) : base(target, eye)
        {
        }

        public override void EnterState(PatrollerStateManager patrollerStateManager)
        {
            Debug.Log("Entering Lost State");
        }

        public override void UpdateState(PatrollerStateManager patrollerStateManager)
        {
            StartPatrolling(true, patrollerStateManager);
            patrollerStateManager.SwitchState(patrollerStateManager.PatrollerPatrollingState);
        }

        public override void OnCollisionEnter(PatrollerStateManager patrollerStateManager, Collision collision)
        {
        }
    }
}