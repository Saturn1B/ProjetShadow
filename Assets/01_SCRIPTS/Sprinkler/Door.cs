using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Door : MonoBehaviour
{
    public Sprinkler[] sprinklers;
    bool open;
    public GameObject doorVisual;

    // Update is called once per frame
    void Update()
    {
        if (!open)
        {
            CheckForSprinkler();
        }
    }

    void CheckForSprinkler()
    {
        int shut = 0;
        foreach (Sprinkler sprinkler in sprinklers)
        {
            if(sprinkler.State == Sprinkler.STATE.OFF)
            {
                shut++;
            }
        }
        if(shut >= sprinklers.Length)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        GetComponent<AudioSource>().Play();
        open = true;
        doorVisual.GetComponent<MeshRenderer>().enabled = false;
        doorVisual.GetComponent<Collider>().enabled = false;
        Destroy(gameObject.transform.GetChild(0).gameObject);
        //Destroy(gameObject);
    }
}
