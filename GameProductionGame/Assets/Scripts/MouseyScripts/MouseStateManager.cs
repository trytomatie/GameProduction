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
    
    public MousePatrolState mousePatrol = new MousePatrolState();
    public MouseIdleState mouseIdle = new MouseIdleState();
    public MouseAnimationState mouseAni = new MouseAnimationState();
    public MouseyCheckForStuff checkForStuff = new MouseyCheckForStuff();
    public MouseyChase mCahse = new MouseyChase();
    MouseBaseState currentState;

    [HideInInspector]
    public Animator mouseAnimator;
    [HideInInspector]
    public NavMeshAgent navMeshMouseAgent;

    [HideInInspector]
    public GameObject player;
    public Transform[] patrolPoints;
    public int nextPatrolPoint;
    
    [HideInInspector]
    public bool forward;
    [HideInInspector]
    public bool inChase;

    public LayerMask mouseRayCastLayers;
    public float mouseyFieldOfView;
    public float eyeHeight;
    public float mouseyViewingDistance;
    

    // Start is called before the first frame update
    void Awake()
    {
        currentState = mouseIdle;
        currentState.EnterMouseState(this);

        navMeshMouseAgent = GetComponent<NavMeshAgent>();
        mouseAnimator = GetComponent<Animator>();
        player = GameObject.Find("Clyde The Kid");

        nextPatrolPoint = 0;
        forward = true;
        inChase = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Check for Player
        checkForStuff.UpdateMouseState(this);

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

    private void OnTriggerEnter(Collider other)
    {
        checkForStuff.MouseTrigger(other, this);
    }
}
