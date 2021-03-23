using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    public bool detectPlayer;
    public bool detectLight;
    public bool runAway;
    public bool disappear;
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
        if (disappear)
        {
            StartCoroutine(DestroyShadow());
        }

        if(LightSource == null || LightSource.activeSelf == false || (LightSource.transform.parent != null && LightSource.transform.parent.gameObject.activeSelf == false) && !disappear)
        {
            detectLight = false;
        }

        if (detectPlayer && !detectLight && !runAway && !disappear)
        {
            Vector3 targetPos = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
        }
        else if (detectPlayer && detectLight && !disappear)
        {
            runAway = true;
        }
        if (runAway && !disappear)
        {
            if(LightSource != null && LightSource.transform.position.x > transform.position.x && first)
            {
                orientation = -orientation;
                first = false;
            }


            if(LightSource == null || LightSource.activeSelf == false || (LightSource.transform.parent != null && LightSource.transform.parent.gameObject.activeSelf == false))
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
            else if((player.position.x > transform.position.x && LightSource.transform.position.x < transform.position.x) || (player.position.x < transform.position.x && LightSource.transform.position.x > transform.position.x))
            {
                if (detectPlayer)
                {
                    runAway = false;
                    orientation = 1;
                    first = true;
                    LightSource = null;
                }
            }

            if(LightSource != null && Vector3.Distance(transform.position, LightSource.transform.position) < distanceFromLight)
            {
                transform.position += new Vector3((speed+5) * Time.deltaTime * orientation, 0, 0);
                /*runAway = false;
                orientation = 1;
                first = true;
                LightSource = null;*/
            }
            else if(LightSource != null && Vector3.Distance(transform.position, LightSource.transform.position) > distanceFromLight)
            {
                transform.position += new Vector3((speed+5) * Time.deltaTime * -orientation, 0, 0);
            }
        }
    }

    IEnumerator DestroyShadow()
    {
        Vector3 back = new Vector3(transform.position.x - (distanceFromLight / 4), transform.position.y + 10, transform.position.z);
        while(transform.position.x > back.x)
        {
            transform.position -= new Vector3((speed) * Time.deltaTime, 0, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        while(transform.position.y < back.y)
        {
            transform.position += new Vector3(0, (speed) * Time.deltaTime, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(gameObject);
    }
}
