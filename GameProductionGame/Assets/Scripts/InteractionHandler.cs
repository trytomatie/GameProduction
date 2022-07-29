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
    public GameObject interactionIndicator;
    public Transform itemAnchor;
    public Transform handIkTarget;
    public Transform lookIkTarget;

    private bool isInteracting = false;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        if (reachableInteractable != null && Helper.DistanceBetween(gameObject, reachableInteractable.gameObject) > interactionDistance && Helper.AngleBetween(gameObject, reachableInteractable.gameObject) < interactionAngleThreshold)
        {
            interactionIndicator.SetActive(true);
            Interactable interactable = reachableInteractable.GetComponent<Interactable>();
            interactionIndicator.transform.position = mainCamera.WorldToScreenPoint(interactable.labelOffset + interactable.transform.position);
            interactionIndicator.GetComponent<TextMeshProUGUI>().text = interactable.interactionName;
            canInteract = true;
        }
        else
        {
            interactionIndicator.SetActive(false);
            canInteract = false;
        }
    }

    public override void UpdateState(GameObject source)
    {
        if(reachableInteractable != null)
        {
            lookIkTarget.transform.position = reachableInteractable.transform.position;
        }
    }


    public override void EnterState(GameObject source)
    {
        if (reachableInteractable.GetComponent<Interactable_Item>() != null)
        {
            anim.SetTrigger("grab");
            handIkTarget.transform.position = reachableInteractable.transform.position + new Vector3(0, 0.08f, 0);

        }
        else if (reachableInteractable.GetComponent<Interactable_KeycardPanel>() != null)
        {
            anim.SetTrigger("interact");
            handIkTarget.transform.position = reachableInteractable.GetComponent<Interactable_KeycardPanel>().ikTarget.transform.position;
        }

        interactionIndicator.SetActive(false);
        isInteracting = true;

        transform.rotation = Quaternion.LookRotation(new Vector3(reachableInteractable.transform.position.x, 0, reachableInteractable.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z), Vector3.up);
    }

    public override void ExitState(GameObject source)
    {
        if (reachableInteractable.GetComponent<Interactable_Item>() != null)
        {
            Destroy(reachableInteractable.gameObject);
        }
        reachableInteractable = null;
    }

    public override StateName Transition(GameObject source)
    {
        if (isInteracting == false)
        {
            return StateName.Controlling;
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
            reachableInteractable.GetComponent<Rigidbody>().isKinematic = true;
        }
        if (ae.stringParameter == "OnGrabComplete" || ae.stringParameter == "OnInteractionComplete")
        {
            anim.SetTrigger("interactionComplete");
            isInteracting = false;
            canInteract = false;
            reachableInteractable.Interaction(gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable") && !isInteracting)
        {
            reachableInteractable = other.gameObject.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item") && !isInteracting)
        {
            reachableInteractable = null;
        }
    }
}
