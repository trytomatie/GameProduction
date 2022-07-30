using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseyCheckForStuff : MouseBaseState
{
    public override void EnterMouseState(MouseStateManager Mouse)
    {

    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        Debug.Log("CheckForStuff");
        Vector3 rayCastOrigin = new Vector3(Mouse.transform.position.x, Mouse.transform.position.y + Mouse.eyeHeight, Mouse.transform.position.z);
        if (CheckPlayerInView(Mouse.player, Mouse) && playerHitByRaycast(Mouse))
        {
            Mouse.SwitchMouseState(Mouse.mCahse);
        }
        
    }

    public override void ExitMouseState(MouseStateManager Mouse)
    {

    }

    private bool playerHitByRaycast(MouseStateManager Mouse)
    {
        Debug.Log("playerHitbyRaycasttest");
        Vector3 rayCastOrigin = new Vector3(Mouse.transform.position.x, Mouse.transform.position.y + Mouse.eyeHeight, Mouse.transform.position.z);
        Debug.DrawRay(rayCastOrigin, (Mouse.transform.forward * Mouse.mouseyViewingDistance), Color.red, Mathf.Infinity);
        RaycastHit hit;

        
        if(Physics.Raycast(rayCastOrigin, Mouse.transform.forward, out hit, Mouse.mouseyViewingDistance, Mouse.mouseRayCastLayers))
        {
            if (hit.collider != null)
            {
                Debug.Log("HitSomething");
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.CompareTag("Player"))
                {
                    Debug.Log("Hitplayer");

                    return true;
                }
                
            }
        }
        
        
            return false;
        
    }

    
    private bool CheckPlayerInView(GameObject player, MouseStateManager Mouse)
    {
        Debug.Log("CheckPlayerInView");
        Vector3 vectorBetween = player.transform.position - Mouse.transform.position;
        float angle = Vector3.Angle(vectorBetween, Mouse.transform.forward);
        
        if (Mouse.mouseyFieldOfView > angle)
        {
            Debug.Log("CheckPlayerInViewTrue");
            return true;
            

        }
            Debug.Log("CheckPlayerInViewFalse");
            return false;
    }
    public void MouseTrigger(Collider other, MouseStateManager Mouse)
    {
        
    }
}
