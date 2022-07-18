using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Item : Interactable
{
    public enum ItemType { Keycard, Cheese};
    public ItemType itemType;
    private void Start()
    {
        interactionName = "Pick up";
    }

    public override void Interaction(GameObject source)
    {
        Inventory inventory = source.GetComponent<Inventory>();
        switch(itemType)
        {
            case ItemType.Keycard:
                inventory.keys++;
                break;
            case ItemType.Cheese:
                inventory.cheese++;
                break;
        }
    }
}

