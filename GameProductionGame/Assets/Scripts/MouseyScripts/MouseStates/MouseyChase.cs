using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseyChase : MouseBaseState
{
    public override void EnterMouseState(MouseStateManager Mouse)
    {

        Mouse.navMeshMouseAgent.speed = 6;
        Mouse.navMeshMouseAgent.SetDestination(Mouse.player.transform.position);

        Mouse.PlayVoiceLines(Mouse.voiceLines[1]);
        Mouse.player.GetComponent<RacerController>().chaseIndex++;



    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        if (Mouse.navMeshMouseAgent.remainingDistance <= 0.2f  /*&& Mouse.navMeshMouseAgent.pathStatus==NavMeshPathStatus.PathComplete*/)
        {
            Mouse.SwitchMouseState(Mouse.mouseySearch);
        }
    }

    public override void ExitMouseState(MouseStateManager Mouse)
    {
        Mouse.player.GetComponent<RacerController>().chaseIndex--;
    }
}
