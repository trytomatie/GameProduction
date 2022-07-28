using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MouseStateManager : MonoBehaviour
{
    MouseBaseState currentState;
    public MousePatrolState mousePatrol = new MousePatrolState();
    public MouseIdleState mouseIdle = new MouseIdleState();

    public Transform[] patrolPoints;
    public int nextPatrolPoint;
    public NavMeshAgent navMeshMouseAgent;
    public bool forward;

    // Start is called before the first frame update
    void Start()
    {
        currentState = mouseIdle;
        currentState.EnterMouseState(this);
        navMeshMouseAgent = GetComponent<NavMeshAgent>();

        nextPatrolPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateMouseState(this);
    }

    public void SwitchMouseState(MouseBaseState newMouseState)
    {
        currentState.ExitMouseState(this);
        currentState = newMouseState;
        currentState.EnterMouseState(this);
    }
}
