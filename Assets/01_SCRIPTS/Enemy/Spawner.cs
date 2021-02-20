using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Vector3 spawnPosition;
    public GameObject MonsterPrefab;
    public GameObject[] Lights;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject monster = Instantiate(MonsterPrefab, spawnPosition, Quaternion.identity);
            foreach (GameObject light in Lights)
            {
                monster.transform.GetChild(2).GetComponent<ParticleSystem>().trigger.AddCollider(light.transform);
            }
            Destroy(gameObject);
        }
    }
}
