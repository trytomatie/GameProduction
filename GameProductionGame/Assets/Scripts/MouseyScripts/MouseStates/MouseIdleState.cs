using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIdleState : MouseBaseState
{
    private float timerseconds;
    private float counter;

    public override void EnterMouseState(MouseStateManager Mouse)
    {
        counter = 0;
        timerseconds = Random.Range(5, 10);
        Debug.Log(timerseconds+"timerSeconds");
        Debug.Log("idle");

        
       
    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        counter = counter + timerseconds * Time.deltaTime;
        Debug.Log(counter);
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
