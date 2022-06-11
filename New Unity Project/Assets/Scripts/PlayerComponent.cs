using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    public bool locked = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LockState(bool state)
    {
        if(state == false && locked == true)
        {
            OnStateEnter();
        }
        locked = state;

    }

    public virtual void OnStateEnter()
    {

    }
}
