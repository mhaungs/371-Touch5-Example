using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum PatrollerStates
{
    patrolling,
    lost,
    chasing
}

public class Patroller : MonoBehaviour
{
    public Transform[] patrolTargets;
    public Transform target;
    public Transform eye;

    private NavMeshAgent agent;
    private int destPoint = 0;
    private IEnumerator coroutine;
    private PatrollerStates _currentState = PatrollerStates.patrolling;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.pathPending ) return;  // Don't continue if still figuring out path

        switch(_currentState )
        {
            case PatrollerStates.patrolling:
                if( CanSeeTarget() )
                {
                    _currentState = PatrollerStates.chasing;
                }
                else
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        coroutine = GoToNextPoint(1);
                        StartCoroutine(coroutine);
                    } 
                }
                break;
            case PatrollerStates.lost:
                coroutine = GoToNextPoint(0);
                StartCoroutine(coroutine);
                _currentState = PatrollerStates.patrolling;
                break;
            case PatrollerStates.chasing:
                if( CanSeeTarget() )
                {
                    agent.SetDestination(target.transform.position);
                }
                else
                {
                    _currentState = PatrollerStates.lost;
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

        destPoint = (destPoint + next) % patrolTargets.Length;
        agent.destination = patrolTargets[destPoint].position;
        agent.isStopped = true;
        yield return new WaitForSeconds(2f);
        agent.isStopped = false;
    }

    private bool CanSeeTarget()
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
