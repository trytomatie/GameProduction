using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseyChase : MouseBaseState
{
    public override void EnterMouseState(MouseStateManager Mouse)
    {

        Mouse.navMeshMouseAgent.speed = 6;
        Mouse.navMeshMouseAgent.SetDestination(Mouse.player.transform.position);
        Debug.Log("HiHi");
    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        if (Mouse.navMeshMouseAgent.remainingDistance <= 0.1f  /*&& Mouse.navMeshMouseAgent.pathStatus==NavMeshPathStatus.PathComplete*/)
        {
            Mouse.SwitchMouseState(Mouse.mouseIdle);
        }
    }

    public override void ExitMouseState(MouseStateManager Mouse)
    {
    }
}
