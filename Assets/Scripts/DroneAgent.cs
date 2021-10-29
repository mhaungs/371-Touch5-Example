using UnityEngine;
using UnityEngine.AI;

public class DroneAgent : MonoBehaviour
{
    public Transform target;
    
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }
}
