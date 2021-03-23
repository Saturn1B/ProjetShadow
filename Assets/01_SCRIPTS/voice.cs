using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class voice : MonoBehaviour
{
    public AudioSource voiceSource;
    public bool isTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTrigger)
        {
            voiceSource.Play();
            isTrigger = true;
        }
    }
}
