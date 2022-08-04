using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseySearchState : MouseBaseState
{

    private Vector3[] lookpoints = new Vector3[6];
    private Vector3 startPoint;
    private int nextPoint;
    private float timer;
    private float counter;

    public override void EnterMouseState(MouseStateManager Mouse)
    {
        timer = 0;
        counter = 0;
        Debug.Log("Searchstate");
        Debug.Log("Lookpoints.length " + lookpoints.Length);
        Mouse.navMeshMouseAgent.speed = 2.5f;
        startPoint = Mouse.transform.position;
        nextPoint = 0;
        for (int i = 0; i < lookpoints.Length; i++)
        {
            float rando = Random.Range(-5,     5);
            lookpoints[i] = new Vector3 (startPoint.x + rando, startPoint.y, startPoint.z + rando);
            Debug.Log(lookpoints[i]);
        }
        Debug.Log(lookpoints);
        Mouse.navMeshMouseAgent.SetDestination(lookpoints[nextPoint]);

        Mouse.PlayVoiceLines(Mouse.voiceLines[3]);
    }

    public override void UpdateMouseState(MouseStateManager Mouse)
    {
        timer += counter * Time.deltaTime; 
        if ( Mouse.navMeshMouseAgent.remainingDistance <= 0.3f && nextPoint < lookpoints.Length-1)
        {
            
            nextPoint++;
            Debug.Log("next LookPoint: " + lookpoints[nextPoint]);
            Mouse.navMeshMouseAgent.SetDestination(lookpoints[nextPoint]);
            Debug.Log(nextPoint);
        } else if (nextPoint >= lookpoints.Length-2 || timer > 10)
        {
            Debug.Log("fertig gesearched");
            Mouse.SwitchMouseState(Mouse.mouseIdle);
        }

    }

    public override void ExitMouseState(MouseStateManager Mouse)
    {
        //Mouse.nextPatrolPoint--;
    }

}
