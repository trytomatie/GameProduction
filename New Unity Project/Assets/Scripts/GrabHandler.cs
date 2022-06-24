using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrabHandler : State
{

    private Animator anim;
    public GameObject reachableItem;
    public bool canGrab = false;
    public float grabdistance = 0.7f;
    public float grabAngleTreshhold = 45;
    public GameObject grabIndicator;
    public Transform itemAnchor;

    private bool isGrabbing = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    /*


public override void OnStateEnter()
{
    anim.SetTrigger("grab");
    grabIndicator.SetActive(false);
    transform.rotation = Quaternion.LookRotation(new Vector3(reachableItem.transform.position.x,0,reachableItem.transform.position.z) - new Vector3(transform.position.x,0,transform.position.z), Vector3.up);
}

public override void OnStateExit()
{
    Destroy(reachableItem);
    reachableItem = null;
}




*/

    // Update is called once per frame
    void Update()
    {
        if (reachableItem != null && Helper.DistanceBetween(gameObject, reachableItem) > grabdistance && Helper.AngleBetween(gameObject, reachableItem) < grabAngleTreshhold)
        {
            grabIndicator.SetActive(true);
            Interactable interactable = reachableItem.GetComponent<Interactable>();
            grabIndicator.transform.position = interactable.labelOffset + interactable.transform.position;
            grabIndicator.GetComponent<TextMeshProUGUI>().text = interactable.interactionName;
            canGrab = true;
        }
        else
        {
            grabIndicator.SetActive(false);
            canGrab = false;
        }
    }
    public override string AnyTransition(GameObject source)
    {
        return "";
    }

    public override void EnterState(GameObject source)
    {
        anim.SetTrigger("grab");
        grabIndicator.SetActive(false);
        isGrabbing = true;
        transform.rotation = Quaternion.LookRotation(new Vector3(reachableItem.transform.position.x, 0, reachableItem.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z), Vector3.up);
    }

    public override void ExitState(GameObject source)
    {
        Destroy(reachableItem);
        reachableItem = null;
    }

    public override string Transition(GameObject source)
    {
        if(isGrabbing == false)
        {
            return "Controlling";
        }
        return stateName;
    }

    public void TriggerAnimationEvent(AnimationEvent ae)
    {
        if (ae.stringParameter == "OnGrabStart")
        {
            reachableItem.transform.parent = itemAnchor;
            reachableItem.transform.localPosition = Vector3.zero;
            reachableItem.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (ae.stringParameter == "OnGrabComplete")
        {
            anim.SetTrigger("grabComplete");
            isGrabbing = false;
            canGrab = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
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
