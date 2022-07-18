using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MousePatrolState : MouseBaseState
{


    private int arrayLength;

    public override void EnterMouseState(MouseStateManager Mouse)
    {
        Debug.Log("Hello from the Patrol State");
        arrayLength = Mouse.patrolPoints.Length;
    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
       
        Mouse.navMeshMouseAgent.SetDestination(Mouse.patrolPoints[Mouse.nextPatrolPoint].position);

        if (Mouse.navMeshMouseAgent.remainingDistance <= 0.1f && Mouse.navMeshMouseAgent.pathStatus==NavMeshPathStatus.PathComplete)
        {
            Mouse.SwitchMouseState(Mouse.mouseIdle);
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
        if (Mouse.nextPatrolPoint == (arrayLength-1))
        {
            Mouse.forward = false;
        }
        if (Mouse.nextPatrolPoint == 0)
        {
            Mouse.forward = true;
        }
    }
}
