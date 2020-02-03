using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroller : MonoBehaviour
{
    public Transform[] patrolTargets;
    public Transform target;
    public Transform eye;

    private NavMeshAgent agent;
    private bool patrolling = false;
    private int destPoint = 0;
    private IEnumerator coroutine;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.pathPending ) return;  // Don't continue if still figuring out path

        if( patrolling )
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                coroutine = GoToNextPoint(1);
                StartCoroutine(coroutine);
            }
        }

        if( CanSeeTarget() )
        {
            agent.SetDestination(target.transform.position);
            patrolling = false;
        }
        else
        {
            if( !patrolling )
            {
                coroutine = GoToNextPoint(0);
                StartCoroutine(coroutine);
            }
        }
    }

    IEnumerator GoToNextPoint(int next)
    {
        if( patrolTargets.Length == 0 )
        {
            // We have no patrol points so quit
            yield break;
        }

        patrolling = true;
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
