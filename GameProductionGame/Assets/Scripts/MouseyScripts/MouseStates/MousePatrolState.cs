using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Markus Schwalb
/// This is the Mouse Patrol State 
/// objective of this script is to the next Patrol point and walk back when the last point is achieved
/// </summary>

public class MousePatrolState : MouseBaseState
{


    

    public override void EnterMouseState(MouseStateManager Mouse)
    {
        //Debug.Log("Hello from the Patrol State");
        Mouse.navMeshMouseAgent.speed=3.5f;
        Mouse.navMeshMouseAgent.SetDestination(Mouse.patrolPoints[Mouse.nextPatrolPoint].position);
    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
       /* Vector3 destination = Mouse.patrolPoints[Mouse.nextPatrolPoint].position;*/
        Mouse.navMeshMouseAgent.SetDestination(Mouse.patrolPoints[Mouse.nextPatrolPoint].position);

        if (Mouse.navMeshMouseAgent.remainingDistance <= 0.1f  /*&& Mouse.navMeshMouseAgent.pathStatus==NavMeshPathStatus.PathComplete*/)
        {
            Mouse.SwitchMouseState(Mouse.mouseIdle);
            Debug.Log("ChangeState");
        } 
        
        
    }

    public override void ExitMouseState(MouseStateManager Mouse)
    {
        CheckForward(Mouse);
        NextPatrolPoint(Mouse);
    }

    private void NextPatrolPoint(MouseStateManager Mouse)
    {
        if (Mouse.forward)
        {
            Mouse.nextPatrolPoint++;
        }
        if (!Mouse.forward)
        {
            Mouse.nextPatrolPoint--;
        }
    }

    private void CheckForward(MouseStateManager Mouse)
    {
        if (Mouse.nextPatrolPoint == (Mouse.patrolPoints.Length - 1))
        {
            Mouse.forward = false;
        }
        if (Mouse.nextPatrolPoint == 0)
        {
            Mouse.forward = true;
        }
    }
}
