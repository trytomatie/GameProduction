using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonStateInteractions : MonoBehaviour
{

    public enum State {Controlling,Grab};
    public State myState;
    private PlayerController controller;
    private GrabHandler grabHandler;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        grabHandler = GetComponent<GrabHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        SetState();
    }

    private void SetState()
    {

        if (grabHandler.canGrab && Input.GetKeyDown(KeyCode.E))
        {
            MyState = State.Grab;
        }

    }

    public State MyState 
    { 
        get => myState;
        set 
        {

            if (myState != value)
            {
                controller.locked = true;
                grabHandler.locked = true;
                myState = value;
                switch (MyState)
                {
                    case State.Controlling:
                        controller.locked = false;
                        controller.OnStateEnter();
                        break;
                    case State.Grab:
                        grabHandler.locked = false;
                        grabHandler.OnStateEnter();
                        break;
                }
            }

        }
        
 
    }
}
