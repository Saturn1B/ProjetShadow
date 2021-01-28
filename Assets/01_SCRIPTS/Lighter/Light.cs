﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Light : MonoBehaviour
{
    public int lightNumber;
    public float lightTimer;
    public GameObject lighter;
    public TextMeshProUGUI lightText;
    public Life life;

    // Update is called once per frame
    void Update()
    {
        if(lightNumber > 0 && Input.GetKeyDown(KeyCode.RightShift) && !lighter.activeSelf)
        {
            lightNumber--;
            lightText.text = lightNumber.ToString();
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
}
