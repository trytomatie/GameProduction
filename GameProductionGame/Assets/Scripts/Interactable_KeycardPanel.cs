using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_KeycardPanel : Interactable
{
    public Animator anim;
    private void Start()
    {
        interactionName = "Interact";
    }

    public override void Interaction(GameObject source)
    {
        Inventory inventory = source.GetComponent<Inventory>();
        if(inventory.keys > 0)
        {
            inventory.keys--;
            anim.SetTrigger("action");
            enabled = false;
        }

    }

    public override void TriggerAnimation(GameObject source)
    {
        source.GetComponent<Animator>().SetTrigger("interact");
    }
}

