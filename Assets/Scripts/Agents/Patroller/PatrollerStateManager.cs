using System;
using System.Collections;
using System.Collections.Generic;
using Agents.Patroller;
using UnityEngine;
using UnityEngine.AI;

public class PatrollerStateManager : MonoBehaviour
{
    
    // Public (set in Unity Inspector)
    public Transform target;
    public Transform eye;
    public Transform[] patrolTargets;

    [HideInInspector]
    public int destPoint = 0;
    
    // Properties
    public NavMeshAgent Agent { get; private set; }
    
    // States
    public PatrollerBaseState currentState;
    public PatrollerChasingState PatrollerChasingState { get; private set; }
    public PatrollerLostState PatrollerLostState { get; private set; }
    public PatrollerPatrollingState PatrollerPatrollingState { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        // initialize states
        PatrollerChasingState = new PatrollerChasingState(target, eye);
        PatrollerLostState = new PatrollerLostState(target, eye);
        PatrollerPatrollingState = new PatrollerPatrollingState(target, eye);
        
        // set Agent
        Agent = GetComponent<NavMeshAgent>();
        
        // set initial state
        currentState = PatrollerPatrollingState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PatrollerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
}
