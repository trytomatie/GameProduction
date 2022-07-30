using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCheeseState : MouseBaseState
{
    private float timer;
    private float counter=1;

    public override void EnterMouseState(MouseStateManager Mouse)
    {
        Mouse.navMeshMouseAgent.speed = 6;
        Mouse.navMeshMouseAgent.SetDestination(Mouse.cheese.transform.position);
        Debug.Log("Cheese");
        timer = 0;
    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        if (Vector3.Distance(Mouse.cheese.transform.position, Mouse.transform.position) < 1)
        {
            Mouse.mouseAnimator.SetBool("EatCheese", true);
            timer += counter * Time.deltaTime;
            if (timer > 15)
            {
                Mouse.SwitchMouseState(Mouse.mouseIdle);
            }
        }
    }

    public override void ExitMouseState(MouseStateManager Mouse)
    {
        Mouse.mouseAnimator.SetBool("EatCheese", false);
    }
}
