using UnityEngine;
using UnityEngine.AI;

namespace Agents.Patroller
{
    public class PatrollerPatrollingState : PatrollerBaseState
    {
        public PatrollerPatrollingState(Transform target, Transform eye) : base(target, eye)
        {
        }

        public override void EnterState(PatrollerStateManager patrollerStateManager)
        {
            Debug.Log("Entering Patrolling State");
        }

        public override void UpdateState(PatrollerStateManager patrollerStateManager)
        {
            NavMeshAgent agent = patrollerStateManager.Agent;
        
            if (CanSeeTarget())
            {
                patrollerStateManager.SwitchState(patrollerStateManager.PatrollerChasingState);
            }
            else
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    StartPatrolling(false, patrollerStateManager);
                }
            }
        }

        public override void OnCollisionEnter(PatrollerStateManager patrollerStateManager, Collision collision)
        {
        }
    }
}