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
    private Vector3 lastKnownPosition;
    private bool patrolling = false;
    private int destPoint = 0;
    private bool arrived = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lastKnownPosition = transform.position;
    }

    void Update()
    {
        if (agent.pathPending ) return;  // Don't continue if still figuring out path

        if( patrolling )
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!arrived)
                {
                    arrived = true;
                    StartCoroutine("GoToNextPoint");
                }
            }
            else
            {
                arrived = false;
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
                agent.SetDestination(lastKnownPosition);
                if( agent.remainingDistance <= agent.stoppingDistance )
                {
                    patrolling = true;
                    StartCoroutine("GoToNextPoint");
                }
            }
        }
    }

    IEnumerator GoToNextPoint()
    {
        if( patrolTargets.Length == 0 )
        {
            // We have no patrol points so quit
            yield break;
        }

        patrolling = true;
        yield return new WaitForSeconds(2f);
        arrived = false;
        agent.destination = patrolTargets[destPoint].position;
        destPoint = (destPoint + 1) % patrolTargets.Length;

    }

    private bool CanSeeTarget()
    {
        bool canSee = false;
        Ray ray = new Ray(eye.position, target.transform.position - eye.position);
        RaycastHit hit;

        if( Physics.Raycast(ray, out hit) )
        {
            canSee = hit.transform == target;
            if (canSee) lastKnownPosition = target.transform.position;
        }

        return canSee;
    }

}
