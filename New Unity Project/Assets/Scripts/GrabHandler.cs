using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHandler : PlayerComponent
{
    private Animator anim;
    public GameObject reachableItem;
    public bool canGrab = false;
    public float grabdistance = 0.7f;
    public float grabAngleTreshhold = 45;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reachableItem != null && Helper.DistanceBetween(gameObject, reachableItem) > grabdistance && Helper.AngleBetween(gameObject, reachableItem) < grabAngleTreshhold)
        {
            canGrab = true;
        }
        else
        {
            canGrab = false;
        }

    }

    public override void OnStateEnter()
    {
        anim.SetTrigger("grab");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            reachableItem = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            reachableItem = null;
        }
    }


}
