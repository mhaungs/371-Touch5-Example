using UnityEngine;

namespace Agents.Patroller
{
    public class PatrollerChasingState : PatrollerBaseState
    {
        public PatrollerChasingState(Transform target, Transform eye) : base(target, eye)
        {
        }

        public override void EnterState(PatrollerStateManager patrollerStateManager)
        {
            Debug.Log("Entering Chasing State");
        }

        public override void UpdateState(PatrollerStateManager patrollerStateManager)
        {
            if (CanSeeTarget())
            {
                patrollerStateManager.Agent.SetDestination(target.transform.position);
            }
            else
            {
                patrollerStateManager.SwitchState(patrollerStateManager.PatrollerLostState);
            }
        }

        public override void OnCollisionEnter(PatrollerStateManager patrollerStateManager, Collision collision)
        {
            Debug.Log("I caught you!!!");
        }
    }
}