using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseyFootsteps : MonoBehaviour
{
    public GameObject audioSourceObj;
    public AudioClip footsteps;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = audioSourceObj.GetComponent<AudioSource>();
        audioSource.clip = footsteps;
    }

    public void Step(AnimationEvent ae)
    {
        audioSource.PlayOneShot(footsteps);
    }

}
