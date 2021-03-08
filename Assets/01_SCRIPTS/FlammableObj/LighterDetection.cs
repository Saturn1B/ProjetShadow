using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LighterDetection : MonoBehaviour
{
    public bool canLight = false;
    lightState lighten = lightState.OFF;
    enum lightState { OFF, ON}
    public GameObject Obj;
    public ParticleSystem Fire;
    public GameObject LightSource;
    Life life;

    public ControlSettings control;

    private void Awake()
    {
        life = GameObject.Find("Player").GetComponent<Life>();
        control = GameObject.Find("ControlSettings").GetComponent<ControlSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canLight)
        {
            if (control.Flame.triggered)
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
        LightSource.SetActive(true);
        yield return new WaitForSeconds(2);
        Obj.GetComponent<Renderer>().material.color = Color.black;
        yield return new WaitForSeconds(1.75f);
        //Fire.gameObject.transform.parent = null;
        Obj.GetComponent<Renderer>().enabled = false;
        Obj.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        life.Light.Remove(LightSource);
        LightSource.SetActive(false);
        Destroy(Obj);
    }
}
