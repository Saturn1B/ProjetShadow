using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    public bool detectPlayer;
    public bool detectLight;
    public bool runAway;
    public Transform player;
    public float speed;
    Vector3 startPos;
    public float distanceFromLight;
    int orientation = 1;
    public GameObject LightSource;
    bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (detectPlayer && !detectLight && !runAway)
        {
            Vector3 targetPos = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
        }
        else if (detectPlayer && detectLight && !runAway)
        {
            runAway = true;
        }
        if (runAway)
        {
            if(LightSource != null && LightSource.transform.position.x > transform.position.x && first)
            {
                orientation = -orientation;
                first = false;
            }
            transform.position += new Vector3(speed * Time.deltaTime * orientation, 0, 0);
            if(LightSource == null)
            {
                runAway = false;
                orientation = 1;
                first = true;
                LightSource = null;
            }
            else if(LightSource != null && !LightSource.activeSelf)
            {
                runAway = false;
                orientation = 1;
                first = true;
                LightSource = null;
            }
            else if(Vector3.Distance(transform.position, LightSource.transform.position) > distanceFromLight)
            {
                runAway = false;
                orientation = 1;
                first = true;
                LightSource = null;
            }
        }
    }
}
