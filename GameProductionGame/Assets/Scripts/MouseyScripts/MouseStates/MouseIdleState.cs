using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Markus Schwalb
/// this script is simply an idle state for waiting time between Patrol points or something
/// </summary>

public class MouseIdleState : MouseBaseState
{
    private float timerseconds;
    private float counter;

    public override void EnterMouseState(MouseStateManager Mouse)
    {
        counter = 0;
        timerseconds = Random.Range(1, 5);
        Debug.Log(timerseconds+"timerSeconds");
        Debug.Log("idle");

        
       
    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        counter = counter + (timerseconds * Time.deltaTime);      //Count the time till the counter time reaches the desired time and than switch back to Patrol points
        //Debug.Log(counter);
        if (counter >= timerseconds)
        {
            Mouse.SwitchMouseState(Mouse.mousePatrol);
        }
    }

    public override void ExitMouseState(MouseStateManager Mouse)
    {
       // base.ExitMouseState();

    }
}
