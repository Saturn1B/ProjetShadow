using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PickableLighter : MonoBehaviour
{
    public int lightNumber;
    public float distance;
    public GameObject player;
    bool canPickUp = false;
    public Lighter lighter;
    public Image lightCounter;
    public Sprite[] numbers;

    public ControlSettings control;

    void Start()
    {
        player = GameObject.Find("Player");
        lighter = player.GetComponent<Lighter>();
        lightCounter = GameObject.Find("LighterImage").GetComponent<Image>();
        control = GameObject.Find("ControlSettings").GetComponent<ControlSettings>();
    }

    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= distance)
        {
            canPickUp = true;
        }

        if(canPickUp && control.Pick.triggered)
        {
            lighter.lightNumber += lightNumber;
            if(lighter.lightNumber > 0) { lightCounter.sprite = numbers[lighter.lightNumber - 1]; lightCounter.color = Color.white; }
            else { lightCounter.sprite = null; lightCounter.color = Color.clear; }
            Destroy(gameObject);
        }
    }

}
