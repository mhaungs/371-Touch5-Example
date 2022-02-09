using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Agents.Patroller
{
    public abstract class PatrollerBaseState
    {
        protected readonly Transform target;
        readonly Transform _eye;

        public PatrollerBaseState(Transform target, Transform eye)
        {
            this.target = target;
            this._eye = eye;
        }
    
        public abstract void EnterState(PatrollerStateManager patrollerStateManager);

        public abstract void UpdateState(PatrollerStateManager patrollerStateManager);

        public abstract void OnCollisionEnter(PatrollerStateManager patrollerStateManager, Collision collision);

        protected bool CanSeeTarget()
        {
            bool canSee = false;
            Vector3 position = _eye.position;
        
            Ray ray = new Ray(position, target.transform.position - position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                canSee = hit.transform == target;
            }

            return canSee;
        }
    
        protected void StartPatrolling(bool returnToCurrentPoint, PatrollerStateManager patrollerStateManager)
        {
            int next = returnToCurrentPoint ? 0 : 1;

            GoToNextPoint(next, patrollerStateManager);
        }
    
        async void GoToNextPoint(int next, PatrollerStateManager patrollerStateManager)
        {
            NavMeshAgent agent = patrollerStateManager.Agent;
            Transform[] patrolTargets = patrollerStateManager.patrolTargets;
        
            if( patrolTargets.Length == 0 )
            {
                // We have no patrol points so quit
                return;
            }

            patrollerStateManager.destPoint = (patrollerStateManager.destPoint + next) % patrolTargets.Length;
            agent.SetDestination(patrolTargets[patrollerStateManager.destPoint].position);
            agent.isStopped = true;
            await Task.Delay(2000);  
            if( Application.isPlaying ) agent.isStopped = false;
        }
    }
}