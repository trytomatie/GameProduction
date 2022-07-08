using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StateMachine : MonoBehaviour
{
    public List<State> states;
    public State currentState;

    // Start is called before the first frame update
    public void Start()
    {
        currentState = states[0];
    }

    public void Update()
    {
        Tick(CheckStates(currentState));
    }

    State CheckStates(State state)
    {
        string stateName = state.stateName;
        foreach (State s in states)
        {
            string name= s.AnyTransition(gameObject);
            if(name != "")
            {
                stateName = name;
            }
            //print(stateName);
        }
        if (state.stateName == stateName)
        {
            stateName = state.Transition(gameObject);
        }
        if(state.stateName != stateName)
        {
            return states.First(state => state.stateName == stateName);
        }
        return state;
    }


    void Tick(State state)
    {
        currentState.UpdateState(gameObject);

        if (state == currentState)
        return;

        currentState?.ExitState(gameObject);
        currentState = state;
        currentState.EnterState(gameObject);
    }
}
