using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    
    public Light lightSource;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 1.5f));
        lightSource.enabled = false;
        yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
        lightSource.enabled = true;
        StartCoroutine(Flash());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
