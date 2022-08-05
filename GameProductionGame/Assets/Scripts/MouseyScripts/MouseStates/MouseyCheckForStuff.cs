using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Markus Schwalb
/// Varios checks if something is in sight usw.
/// </summary>
public class MouseyCheckForStuff : MouseBaseState
{
    private float distance;

    public override void EnterMouseState(MouseStateManager Mouse)
    {

    }


    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        calculateDistance(Mouse);

        //Debug.Log("CheckForStuff");
        Vector3 rayCastOrigin = new Vector3(Mouse.transform.position.x, Mouse.transform.position.y + Mouse.eyeHeight, Mouse.transform.position.z);
        if (CheckPlayerInView(Mouse.player, Mouse))
        {
            if (Mouse.currentState != Mouse.mouseCheese)
            {
                Mouse.SwitchMouseState(Mouse.mChase);
            }
        }

        CheckForNoise(Mouse);
    }


    public override void ExitMouseState(MouseStateManager Mouse)
    {

    }

    private bool CheckPlayerInView(GameObject player, MouseStateManager Mouse)
    {
        Debug.Log("CheckPlayerInView");
        Vector3 vectorBetween = player.transform.position - Mouse.transform.position;
        float angle = Vector3.Angle(vectorBetween, Mouse.transform.forward);

        if (Mouse.mouseyFieldOfView > angle)
        {
            //Debug.Log("CheckPlayerInViewTrue");
            if (playerHitByRaycast(Mouse, vectorBetween))
            {
                return true;
            }

        }
        Debug.Log("CheckPlayerInViewFalse");
        return false;
    }

    private bool playerHitByRaycast(MouseStateManager Mouse, Vector3 vectorToPlayer)
    {
        Debug.Log("playerHitbyRaycasttest");
        Vector3 rayCastOrigin = new Vector3(Mouse.transform.position.x, Mouse.transform.position.y + Mouse.eyeHeight, Mouse.transform.position.z);
        
        RaycastHit hit;

        
        if(Physics.Raycast(rayCastOrigin, vectorToPlayer, out hit, Mouse.mouseyViewingDistance, Mouse.mouseRayCastLayers))
        {
            if (hit.collider != null)
            {
                //Debug.Log("HitSomething");
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Player"))
                {
                    //Debug.Log("Hitplayer");
                    //Debug.DrawRay(rayCastOrigin, (vectorToPlayer.normalized * hit.distance), Color.green);
                    return true;
                }
                
            }
        }
        Debug.DrawRay(rayCastOrigin, (vectorToPlayer.normalized * Mouse.mouseyViewingDistance), Color.red);

        return false;
        
    }

    
    /// <summary>
    /// Check if collided thing is a cheese if so the Mouse gets Distracted and chases after the Cheese a Mouse can smell cheese therefore it doesnt have to see it ;D
    /// </summary>
    /// <param name="other"></param>
    /// <param name="Mouse"></param>
   public void MouseTrigger(Collider other, MouseStateManager Mouse)
    {
        if (Mouse.currentState != Mouse.mChase)
        {
            if (other.gameObject.GetComponent<Interactable_Item>() != null)
            {
                GameObject item = other.gameObject;
                Interactable_Item itemScript = item.GetComponent<Interactable_Item>();
                if (itemScript.itemType == Interactable_Item.ItemType.Cheese)
                {
                    Mouse.cheese = item;
                    Mouse.SwitchMouseState(Mouse.mouseCheese);
                }
            }
        }
        
    }

    private void calculateDistance(MouseStateManager Mouse)
    {
        distance = Vector3.Distance(Mouse.player.transform.position, Mouse.transform.position);
        if (Mouse.currentState == Mouse.mChase && distance < Mouse.catchDistance)
        {
            SceneManager.LoadScene(2);
            Debug.Log("verloren");
        }
    }

    private void CheckForNoise(MouseStateManager Mouse)
    {
        
        RacerController player = Mouse.player.GetComponent<RacerController>();
        if (player.noise > distance)
        {
            Mouse.SwitchMouseState(Mouse.mChase);
        }
    }
}
