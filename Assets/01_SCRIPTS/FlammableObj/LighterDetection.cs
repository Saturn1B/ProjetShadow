using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterDetection : MonoBehaviour
{
    public bool canLight = false;
    public GameObject Obj;
    public ParticleSystem Fire;

    // Update is called once per frame
    void Update()
    {
        if (canLight)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartCoroutine(EnlightObject());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("salut");
        if (other.CompareTag("Lighter"))
        {
            Debug.Log("salut2");
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
