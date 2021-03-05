﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed;
    public Vector3 offSet;
    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y + offSet.y, transform.position.z);
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offSet;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
