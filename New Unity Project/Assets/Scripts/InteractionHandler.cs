using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionHandler : State
{

    private Animator anim;
    public Interactable reachableInteractable;
    public bool canInteract = false;
    public float interactionDistance = 0.7f;
    public float interactionAngleThreshold = 45;
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
        if (reachableInteractable != null && Helper.DistanceBetween(gameObject, reachableInteractable.gameObject) > interactionDistance && Helper.AngleBetween(gameObject, reachableInteractable.gameObject) < interactionAngleThreshold)
        {
            grabIndicator.SetActive(true);
            Interactable interactable = reachableInteractable.GetComponent<Interactable>();
            grabIndicator.transform.position = interactable.labelOffset + interactable.transform.position;
            grabIndicator.GetComponent<TextMeshProUGUI>().text = interactable.interactionName;
            canInteract = true;
        }
        else
        {
            grabIndicator.SetActive(false);
            canInteract = false;
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
        transform.rotation = Quaternion.LookRotation(new Vector3(reachableInteractable.transform.position.x, 0, reachableInteractable.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z), Vector3.up);
    }

    public override void ExitState(GameObject source)
    {
        Destroy(reachableInteractable);
        reachableInteractable = null;
    }

    public override string Transition(GameObject source)
    {
        if(isGrabbing == false)
        {
            return "controlling";
        }
        return stateName;
    }

    public void TriggerAnimationEvent(AnimationEvent ae)
    {
        if (ae.stringParameter == "OnGrabStart")
        {
            reachableInteractable.transform.parent = itemAnchor;
            reachableInteractable.transform.localPosition = Vector3.zero;
            reachableInteractable.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (ae.stringParameter == "OnGrabComplete")
        {
            anim.SetTrigger("grabComplete");
            isGrabbing = false;
            canInteract = false;
            reachableInteractable.Interaction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item") && !isGrabbing)
        {
            reachableInteractable = other.gameObject.GetComponent<Interactable>();
        }
    }




    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item") && !isGrabbing)
        {
            reachableInteractable = null;
        }
    }
}
