using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public float mentalHealthMax;
    public float currentMentalHealth;
    public List<GameObject> Light = new List<GameObject>();
    List<GameObject> Follow = new List<GameObject>();
    List<GameObject> Shadow = new List<GameObject>();
    float mentalGain = -0.01f;
    public Slider mentalHealthSlider;
    public GameObject CheckPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentMentalHealth = mentalHealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        //test for mentalGain
        #region
        if (Light.Count > 0 && Follow.Count <= 0 && Shadow.Count <= 0)
        {
            mentalGain = 0.1f;
        }
        else if (Light.Count <= 0 && Follow.Count > 0 && Shadow.Count <= 0)
        {
            mentalGain = -0.1f;

        }
        else if (Light.Count <= 0 && Follow.Count <= 0 && Shadow.Count > 0)
        {
            mentalGain = -0.5f;

        }
        else if (Light.Count > 0 && Follow.Count > 0 && Shadow.Count <= 0)
        {
            mentalGain = 0.1f;
        }
        else if (Light.Count > 0 && Follow.Count <= 0 && Shadow.Count > 0)
        {
            mentalGain = 0.1f;
        }
        else if (Light.Count <= 0 && Follow.Count > 0 && Shadow.Count > 0)
        {
            mentalGain = -0.5f;
        }
        else if (Light.Count > 0 && Follow.Count > 0 && Shadow.Count > 0)
        {
            mentalGain = 0.1f;
        }
        else if (Light.Count <= 0 && Follow.Count <= 0 && Shadow.Count <= 0)
        {
            mentalGain = -0.01f;
        }
        #endregion

        if (currentMentalHealth > 0 && currentMentalHealth <= mentalHealthMax)
        {
            currentMentalHealth += mentalGain;
        }
        else if(currentMentalHealth <= 0)
        {
            if(CheckPoint != null)
            {
                transform.position = new Vector3(CheckPoint.transform.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = Vector3.zero;
            }
            //currentMentalHealth = mentalHealthMax;
            StartCoroutine(RegenMental());
        }
        else if(currentMentalHealth > mentalHealthMax)
        {
            currentMentalHealth = mentalHealthMax;
        }

        mentalHealthSlider.value = currentMentalHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light")) 
        {
            Light.Add(other.gameObject);
        }
        if (other.CompareTag("Follow"))
        {
            Follow.Add(other.gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            Shadow.Add(other.gameObject);
        }

        if (other.CompareTag("Checkpoint"))
        {
            if(CheckPoint != null && CheckPoint != other.gameObject)
            {
                Destroy(CheckPoint);
            }
            CheckPoint = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            Light.Remove(other.gameObject);
        }
        if (other.CompareTag("Follow"))
        {
            Follow.Remove(other.gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            Shadow.Remove(other.gameObject);
        }
    }

    IEnumerator RegenMental()
    {
        yield return new WaitForSeconds(0.02f);
        currentMentalHealth = mentalHealthMax;
    }
}
