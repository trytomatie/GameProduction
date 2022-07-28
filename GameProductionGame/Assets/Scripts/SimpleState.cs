using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleState : ScriptableObject
{

    public string stateName;

    public virtual void EnterState(GameObject source)
    {

    }

    public virtual void UpdateState()
    {

    }

    public virtual string Transition()
    {
        return stateName;
    }

    public virtual void ExitState()
    {

    }

    public virtual string AnyTransition()
    {
        return "";
    }
}
