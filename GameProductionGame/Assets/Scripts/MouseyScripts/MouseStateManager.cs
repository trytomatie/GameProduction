using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Markus Schwalb
/// This is the state mashine that runsthe Mouse states
/// </summary>

public class MouseStateManager : MonoBehaviour
{
    MouseBaseState currentState;
    public MousePatrolState mousePatrol = new MousePatrolState();
    public MouseIdleState mouseIdle = new MouseIdleState();
    public MouseAnimationState mouseAni = new MouseAnimationState();

    public Transform[] patrolPoints;
    public int nextPatrolPoint;
    public NavMeshAgent navMeshMouseAgent;
    public bool forward;
    public Animator mouseAnimator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = mouseIdle;
        currentState.EnterMouseState(this);

        navMeshMouseAgent = GetComponent<NavMeshAgent>();
        mouseAnimator = GetComponent<Animator>();

        nextPatrolPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Update current state
        currentState.UpdateMouseState(this);

        //update the animations
        mouseAni.UpdateMouseState(this);
    }


    /// <summary>
    /// this method carrys out hte exit state of the currenstate than loads the next state and plays the enter state of the new state
    /// </summary>
    /// <param name="newMouseState"></param>
    public void SwitchMouseState(MouseBaseState newMouseState)
    {
        currentState.ExitMouseState(this);
        currentState = newMouseState;
        currentState.EnterMouseState(this);
    }
}
