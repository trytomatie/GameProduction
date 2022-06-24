using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{

    public string stateName;

    public virtual void EnterState(GameObject source)
    {

    }

    public virtual void UpdateState(GameObject source)
    {

    }

    public virtual string Transition(GameObject source)
    {
        return stateName;
    }

    public virtual void ExitState(GameObject source)
    {

    }

    public virtual string AnyTransition(GameObject source)
    {
        return "";
    }
}
