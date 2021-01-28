using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickableLighter : MonoBehaviour
{
    public int lightNumber;
    public float distance;
    public GameObject player;
    bool canPickUp = false;
    public Light lighter;
    public TextMeshProUGUI lightText;

    void Start()
    {
        player = GameObject.Find("Player");
        lighter = player.GetComponent<Light>();
        lightText = GameObject.Find("LightNumber").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) <= distance)
        {
            Debug.Log("pickUp");
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
