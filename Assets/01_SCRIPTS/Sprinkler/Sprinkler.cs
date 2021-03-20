using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sprinkler : MonoBehaviour
{
    public Sprinkler[] neighboorSprinkler;
    public enum STATE { ON, OFF};
    public STATE State;

    // Start is called before the first frame update
    void Awake()
    {
        if(State == STATE.OFF)
        {
            gameObject.GetComponent<ParticleSystem>().Stop();
            gameObject.GetComponent<AudioSource>().Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lighter") && State == STATE.OFF)
        {
            StartCoroutine(ActivateSprinkler());
        }
    }

    void CheckNeighboor()
    {
        foreach (Sprinkler sprinkler in neighboorSprinkler)
        {
            if(sprinkler.State == STATE.OFF)
            {
                sprinkler.State = STATE.ON;
                sprinkler.gameObject.GetComponent<ParticleSystem>().Play();
                sprinkler.gameObject.GetComponent<AudioSource>().Play();
            }
            else
            {
                sprinkler.State = STATE.OFF;
                sprinkler.gameObject.GetComponent<ParticleSystem>().Stop();
                sprinkler.gameObject.GetComponent<AudioSource>().Stop();
            }
        }
    }

    IEnumerator ActivateSprinkler()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
        CheckNeighboor();
        State = STATE.ON;
    }
}
