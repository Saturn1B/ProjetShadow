using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterDetection : MonoBehaviour
{
    public bool canLight = false;
    lightState lighten = lightState.OFF;
    enum lightState { OFF, ON}
    public GameObject Obj;
    public ParticleSystem Fire;
    public GameObject LightSource;

    // Update is called once per frame
    void Update()
    {
        if (canLight)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                lighten = lightState.ON;
                StartCoroutine(EnlightObject());
            }
        }

        switch (lighten)
        {
            case lightState.OFF:
                    break;
            case lightState.ON:
                LightSource.SetActive(true);
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lighter"))
        {
            canLight = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lighter"))
        {
            canLight = false;
        }
    }

    IEnumerator EnlightObject()
    {
        Fire.Play();
        yield return new WaitForSeconds(2);
        Obj.GetComponent<Renderer>().material.color = Color.black;
        yield return new WaitForSeconds(1.75f);
        //Fire.gameObject.transform.parent = null;
        Obj.GetComponent<Renderer>().enabled = false;
        Obj.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        Destroy(Obj);
    }
}
