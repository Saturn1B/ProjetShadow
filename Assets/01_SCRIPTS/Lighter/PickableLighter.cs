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
    public Text lightText;

    public ControlSettings control;

    void Start()
    {
        player = GameObject.Find("Player");
        lighter = player.GetComponent<Lighter>();
        lightText = GameObject.Find("LightNumber").GetComponent<Text>();
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
            lightText.text = (lighter.lightNumber).ToString();
            Destroy(gameObject);
        }
    }

}
