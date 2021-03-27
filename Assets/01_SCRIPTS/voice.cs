using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class voice : MonoBehaviour
{
    public AudioSource voiceSource;
    public bool isTrigger;
    public string voiceText;
    public float time;
    TextMeshProUGUI text;

    private void Start()
    {
        text = GameObject.Find("VoiceDisplay").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTrigger)
        {
            voiceSource.Play();
            StartCoroutine(Subtitle());
            isTrigger = true;
        }
    }

    IEnumerator Subtitle()
    {
        text.text = voiceText;
        yield return new WaitForSeconds(time);
        text.text = "";
    }
}
