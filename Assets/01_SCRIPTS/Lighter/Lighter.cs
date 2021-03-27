using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Lighter : MonoBehaviour
{
    public int lightNumber;
    public float lightTimer;
    public GameObject lighter;
    public Life life;
    public Image lightCounter;
    public Sprite[] numbers;

    public ControlSettings control;

    private void Start()
    {
        control = GameObject.Find("ControlSettings").GetComponent<ControlSettings>();
        if (lightNumber > 0) { lightCounter.sprite = numbers[lightNumber - 1]; lightCounter.color = Color.white; }
        else { lightCounter.sprite = null; lightCounter.color = Color.clear; }
    }

    // Update is called once per frame
    void Update()
    {
        if(lightNumber > 0 && control.Light.triggered && !lighter.activeSelf)
        {
            lightNumber--;
            if (lightNumber > 0) { lightCounter.sprite = numbers[lightNumber - 1]; lightCounter.color = Color.white; }
            else { lightCounter.sprite = null; lightCounter.color = Color.clear; }
            lighter.SetActive(true);
            life.Light.Add(lighter);
            StartCoroutine(LighterTimer());
        }
    }

    IEnumerator LighterTimer()
    {
        yield return new WaitForSeconds(lightTimer);
        life.Light.Remove(lighter);
        lighter.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sprinkler"))
        {
            if(other.GetComponent<Sprinkler>().State == Sprinkler.STATE.ON)
            {
                lighter.SetActive(false);

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Sprinkler"))
        {
            if (other.GetComponent<Sprinkler>().State == Sprinkler.STATE.ON)
            {
                lighter.SetActive(false);

            }
        }
    }
}
