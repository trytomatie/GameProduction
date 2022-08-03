using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionName;
    public Vector3 labelOffset;

    public Transform ikTarget;
    public GameObject reticle;
    private GameObject reticleInstance;
    private Animator reticleAnimator;
    private Transform reticleHolder;

    private void Awake()
    {
        reticleHolder = GameObject.Find("ReticleHolder").transform;
    }


    private bool isReachable;
    public virtual void Interaction(GameObject soruce)
    {

    }


    public virtual void TriggerAnimation(GameObject source)
    {

    }

    public bool IsReachable
    {
        get => isReachable;
        set
        {
            if (value != isReachable)
            {
                if (value == true)
                {
                    SpawnReticle();
                }
                else
                {
                    DespawnReticle();
                }

            }
            isReachable = value;
        }
    }

    private void SpawnReticle()
    {
        if (reticleInstance == null)
        {
            reticleInstance = Instantiate(reticle, transform.position, Quaternion.identity, reticleHolder);
            reticleInstance.GetComponent<ReticleUI>().target = transform;
            reticleAnimator = reticleInstance.GetComponent<Animator>();
            reticleInstance.GetComponentInChildren<TextMeshProUGUI>().text = interactionName;
        }
    }

    private void DespawnReticle()
    {
        reticleAnimator.SetTrigger("Despawn");
        Destroy(reticleInstance, 0.5f);
        reticleInstance = null;
    }

    public void ShowReticleText()
    {
        if (reticleAnimator != null) reticleAnimator.SetBool("ShowText", true);

    }

    public void HideReticleText()
    {
        if (reticleAnimator != null) reticleAnimator.SetBool("ShowText", false);
    }

}

