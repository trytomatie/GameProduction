using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCheeseState : MouseBaseState
{
    private float timer;
    private float counter=1;
    private Transform cheeseTransform;

    public override void EnterMouseState(MouseStateManager Mouse)
    {
        cheeseTransform = Mouse.cheese.transform;
        Mouse.navMeshMouseAgent.speed = 6;
        Mouse.navMeshMouseAgent.SetDestination(Mouse.cheese.transform.position);

        Debug.Log("Cheese");
        timer = 0;
        Mouse.PlayVoiceLines(Mouse.voiceLines[2]);
    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        Debug.Log("cheeseUpdate");
        if (Vector3.Distance(cheeseTransform.position, Mouse.transform.position) < 4)
        {
            Mouse.mouseAnimator.SetBool("EatCheese", true);
            Mouse.cheese.GetComponent<Animator>().SetBool("isEaten", true);
        }
        Debug.Log("AnimatorSet");
        if (!Mouse.cheese.GetComponent<Animator>().GetBool("isEaten"))
        {
            timer = 0;
        }

        timer += counter * Time.deltaTime;

        Debug.Log("timer" + timer);

        if (timer > 3.5f)
        {
            Mouse.SwitchMouseState(Mouse.mouseIdle);
        }
    }

    public override void ExitMouseState(MouseStateManager Mouse)
    {
        Mouse.mouseAnimator.SetBool("EatCheese", false);
        Debug.Log("cheeseExit");

        Mouse.cheese = null;
        GameObject.Destroy(Mouse.cheese);
        //GameObject.Destroy(Mouse.cheese);


    }
}
