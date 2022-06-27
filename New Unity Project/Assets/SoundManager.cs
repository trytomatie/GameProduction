using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void TriggerSoundEvent(string soundName)
    {
        audioSource.clip = clips.First(e => e.name == soundName);
        audioSource.Play();
        audioSource.pitch = Random.Range(0.9f,1.1f);
    }
}
