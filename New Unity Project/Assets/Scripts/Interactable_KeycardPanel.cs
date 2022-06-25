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

    public override void Interaction()
    {
        anim.SetTrigger("action");
    }
}

