using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed;
    public Vector3 offSet;
    Vector3 velocity = Vector3.zero;
    bool canMove;

    private void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y + offSet.y, transform.position.z);
    }

    private void Update()
    {
        if(player.position.x < -205)
        {
            canMove = false;
        }
        else if(player.position.x > 1403)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    void LateUpdate()
    {
        if(canMove)
        {
            Vector3 desiredPosition = player.position + offSet;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;
        }
        //Mathf.Clamp(transform.position.x, -205, 1403);
    }
}
