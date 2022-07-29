using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Markus Schwalb
/// This is the script dor Updating the Animation Mainly the Walk animation
/// </summary>

public class MouseAnimationState : MouseBaseState
{
    public override void EnterMouseState(MouseStateManager Mouse)
    {

    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        float speed = Mouse.navMeshMouseAgent.velocity.magnitude;
        Mouse.mouseAnimator.SetFloat("Direction", speed);
        //Debug.Log("Gonna go fast "+ Mouse.navMeshMouseAgent.velocity);
    }

    public override void ExitMouseState(MouseStateManager Mouse)
    {
    }
}
