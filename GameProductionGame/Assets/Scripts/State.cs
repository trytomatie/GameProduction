using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public enum StateName { Empty,Controlling,Interacting,Throwing}
    public StateName stateName;

    public virtual void EnterState(GameObject source)
    {

    }

    public virtual void UpdateState(GameObject source)
    {

    }

    public virtual StateName Transition(GameObject source)
    {
        return stateName;
    }

    public virtual void ExitState(GameObject source)
    {

    }

    public virtual StateName AnyTransition(GameObject source)
    {
        return StateName.Empty;
    }
}
