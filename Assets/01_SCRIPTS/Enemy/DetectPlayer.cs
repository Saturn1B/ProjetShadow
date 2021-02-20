using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public ShadowMovement shadowMovement;
    public Camera MainCamera;
    Color baseColor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shadowMovement.detectPlayer = true;
            shadowMovement.player = other.transform;
            MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            //StartCoroutine(DownColor());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shadowMovement.detectPlayer = false;
            //shadowMovement.player = null;
            //StartCoroutine(RiseColor());
        }
    }

    IEnumerator DownColor()
    {
        Color color = new Color(0.1f, 0.1f, 0.1f);
        baseColor = MainCamera.backgroundColor;
        while (MainCamera.backgroundColor.b >= color.b)
        {
            MainCamera.backgroundColor -= new Color(0.002f, 0.002f, 0.002f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        MainCamera.backgroundColor = color;
    }

    IEnumerator RiseColor()
    {
        while (MainCamera.backgroundColor.b <= baseColor.b)
        {
            MainCamera.backgroundColor += new Color(0.002f, 0.002f, 0.002f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        MainCamera.backgroundColor = baseColor;
    }
}
