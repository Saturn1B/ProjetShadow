using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickableLighter : MonoBehaviour
{
    public int lightNumber;
    public float distance;
    public GameObject player;
    bool canPickUp = false;
    public Lighter lighter;
    public Text lightText;

    void Start()
    {
        player = GameObject.Find("Player");
        lighter = player.GetComponent<Lighter>();
        lightText = GameObject.Find("LightNumber").GetComponent<Text>();
    }

    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= distance)
        {
            canPickUp = true;
        }

        if(canPickUp && Input.GetKeyDown(KeyCode.RightControl))
        {
            lighter.lightNumber += lightNumber;
            lightText.text = (lighter.lightNumber).ToString();
            Destroy(gameObject);
        }
    }

}
