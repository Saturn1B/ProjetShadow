using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprinkler[] sprinklers;
    bool open;

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
        open = true;
        Destroy(gameObject);
    }
}
