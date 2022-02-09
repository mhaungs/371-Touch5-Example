using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Transition State Diagram:
// Initial State: Patrolling
// Patrolling -> Chasing -> Lost -> Patrolling

enum PatrollerStates
{
    Patrolling,
    Lost,
    Chasing
}

public class Patroller : MonoBehaviour
{
    public Transform[] patrolTargets;
    public Transform target;
    public Transform eye;

    NavMeshAgent _agent;
    int _destPoint = 0;
    IEnumerator _coroutine;
    PatrollerStates _currentState = PatrollerStates.Patrolling;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_agent.pathPending ) return;  // Don't continue if still figuring out path

        switch(_currentState )
        {
            case PatrollerStates.Patrolling:
                if( CanSeeTarget() )
                {
                    _currentState = PatrollerStates.Chasing;
                }
                else
                {
                    if (_agent.remainingDistance <= _agent.stoppingDistance)
                    {
                        _coroutine = GoToNextPoint(1);
                        StartCoroutine(_coroutine);
                    } 
                }
                break;
            case PatrollerStates.Lost:
                _coroutine = GoToNextPoint(0);
                StartCoroutine(_coroutine);
                _currentState = PatrollerStates.Patrolling;
                break;
            case PatrollerStates.Chasing:
                if( CanSeeTarget() )
                {
                    _agent.SetDestination(target.transform.position);
                }
                else
                {
                    _currentState = PatrollerStates.Lost;
                }
                break;
        }
    }

    IEnumerator GoToNextPoint(int next)
    {
        if( patrolTargets.Length == 0 )
        {
            // We have no patrol points so quit
            yield break;
        }

        _destPoint = (_destPoint + next) % patrolTargets.Length;
        _agent.SetDestination(patrolTargets[_destPoint].position);
        _agent.isStopped = true;
        yield return new WaitForSeconds(2f);
        _agent.isStopped = false;
    }

    bool CanSeeTarget()
    {
        bool canSee = false;
        Ray ray = new Ray(eye.position, target.transform.position - eye.position);
        RaycastHit hit;

        if( Physics.Raycast(ray, out hit) )
        {
            canSee = hit.transform == target;
        }

        return canSee;
    }

}
