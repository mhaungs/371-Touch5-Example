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

[RequireComponent(typeof(NavMeshAgent))]
public class Patroller : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolTargets;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _eye;

    private NavMeshAgent _agent;
    private int _destPoint = 0;
    private PatrollerStates _currentState = PatrollerStates.Patrolling;
    private bool _isWaitingAtWaypoint = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (_patrolTargets == null || _patrolTargets.Length == 0 || _patrolTargets[0] == null)
            return;

        _agent.SetDestination(_patrolTargets[_destPoint].position);
    }

    private void Update()
    {
        if (_agent.pathPending)
        {
            return;  // Don't continue if still figuring out path
        }

        if(_isWaitingAtWaypoint)
        {
            return;
        }

        switch (_currentState )
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
                        StartCoroutine(GoToNextPoint(true));
                    } 
                }
                break;
            case PatrollerStates.Lost:
                StartCoroutine(GoToNextPoint(false));
                _currentState = PatrollerStates.Patrolling;
                break;
            case PatrollerStates.Chasing:
                if( CanSeeTarget() )
                {
                    _agent.SetDestination(_target.transform.position);
                }
                else
                {
                    _currentState = PatrollerStates.Lost;
                }
                break;
        }
    }

    private IEnumerator GoToNextPoint(bool advanceToNext)
    {
        Debug.Log("GotoNextPoint Cooroutine called.");
        if ( _patrolTargets.Length == 0 )
        {
            // We have no patrol points so quit
            yield break;
        }
        
        //int next = _currentState == PatrollerStates.Patrolling ? 1 : 0;
        int next = advanceToNext ? 1 : 0;

        _destPoint = (_destPoint + next) % _patrolTargets.Length;
        _agent.SetDestination(_patrolTargets[_destPoint].position);
        _agent.isStopped = true;
        _isWaitingAtWaypoint = true;
        yield return new WaitForSeconds(2f);
        _isWaitingAtWaypoint = false;
        _agent.isStopped = false;
    }

    private bool CanSeeTarget()
    {
        if(_target == null || _eye == null)
        {
            return false;
        }

        Vector3 direction = _target.transform.position - _eye.position;
        float distance = direction.magnitude;

        bool canSee = false;
        Ray ray = new Ray(_eye.position, direction.normalized);

        if( Physics.Raycast(ray, out RaycastHit hit, distance) )
        {
            canSee = hit.transform == _target;
        }

        return canSee;
    }

}
