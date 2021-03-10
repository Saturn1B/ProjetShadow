using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Life : MonoBehaviour
{
    public float mentalHealthMax;
    public float currentMentalHealth;
    public List<GameObject> Light = new List<GameObject>();
    List<GameObject> Follow = new List<GameObject>();
    List<GameObject> Shadow = new List<GameObject>();
    float mentalGain = -0.02f;
    public Slider mentalHealthSlider;
    public GameObject CheckPoint;
    public PlayerMovement playerMovement;
    public bool isPaused;

    float targetSpeed;

    private void Awake()
    {
        StartCoroutine(StartLevel());
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMentalHealth = mentalHealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        //test for mentalGain
        #region
        if (Light.Count > 0 && Follow.Count <= 0 && Shadow.Count <= 0)
        {
            mentalGain = 0.1f;
            targetSpeed = playerMovement.walkSpeed;
        }
        else if (Light.Count <= 0 && Follow.Count > 0 && Shadow.Count <= 0)
        {
            mentalGain = -0.1f;
            targetSpeed = playerMovement.fearSpeed;
        }
        else if (Light.Count <= 0 && Follow.Count <= 0 && Shadow.Count > 0)
        {
            mentalGain = -1f;
            targetSpeed = playerMovement.sprintSpeed;
        }
        else if (Light.Count > 0 && Follow.Count > 0 && Shadow.Count <= 0)
        {
            mentalGain = 0.1f;
            targetSpeed = playerMovement.fearSpeed;
        }
        else if (Light.Count > 0 && Follow.Count <= 0 && Shadow.Count > 0)
        {
            mentalGain = 0.1f;
            targetSpeed = playerMovement.sprintSpeed;
        }
        else if (Light.Count <= 0 && Follow.Count > 0 && Shadow.Count > 0)
        {
            mentalGain = -1f;
            targetSpeed = playerMovement.sprintSpeed;
        }
        else if (Light.Count > 0 && Follow.Count > 0 && Shadow.Count > 0)
        {
            mentalGain = 0.1f;
            targetSpeed = playerMovement.walkSpeed;
        }
        else if (Light.Count <= 0 && Follow.Count <= 0 && Shadow.Count <= 0)
        {
            mentalGain = -0.02f;
            targetSpeed = playerMovement.walkSpeed;
        }
        #endregion

        if (playerMovement.moveSpeed != targetSpeed)
        {
            StartCoroutine(SpeedChange(targetSpeed));
        }

        if (currentMentalHealth > 0 && currentMentalHealth <= mentalHealthMax && !isPaused)
        {
            currentMentalHealth += mentalGain;
        }
        else if(currentMentalHealth <= 1)
        {
            SceneManager.LoadScene(1);
            if(CheckPoint != null)
            {
                //playerMovement.enabled = false;
                //transform.position = CheckPoint.transform.position;//new Vector3(CheckPoint.transform.position.x, transform.position.y, transform.position.z);
                //playerMovement.enabled = true;
            }
            else
            {
                //playerMovement.enabled = false;
                //transform.position = Vector3.zero;
                //playerMovement.enabled = true;
            }
            //currentMentalHealth = mentalHealthMax;
            //StartCoroutine(RegenMental());
        }
        else if(currentMentalHealth > mentalHealthMax)
        {
            currentMentalHealth = mentalHealthMax;
        }

        mentalHealthSlider.value = currentMentalHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light")) 
        {
            Light.Add(other.gameObject);
        }
        if (other.CompareTag("Follow"))
        {
            Follow.Add(other.gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            Shadow.Add(other.gameObject);
        }

        if (other.CompareTag("Checkpoint"))
        {
            if(CheckPoint != null && CheckPoint != other.gameObject)
            {
                CheckPoint.SetActive(false);
            }
            CheckPoint = other.gameObject;
            PlayerPrefs.SetFloat("X", CheckPoint.transform.position.x);
            PlayerPrefs.SetFloat("Y", transform.position.y);
            PlayerPrefs.SetFloat("Z", transform.position.z);
            Debug.Log(PlayerPrefs.GetFloat("X") + " " + PlayerPrefs.GetFloat("Y") + " " + PlayerPrefs.GetFloat("Z"));
        }

        if (other.CompareTag("Death"))
        {
            currentMentalHealth = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            Light.Remove(other.gameObject);
        }
        if (other.CompareTag("Follow"))
        {
            Follow.Remove(other.gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            Shadow.Remove(other.gameObject);
        }
    }

    IEnumerator RegenMental()
    {
        yield return new WaitForSeconds(0.02f);
        currentMentalHealth = mentalHealthMax;
    }

    IEnumerator SpeedChange(float targetSpeed)
    {
        if (playerMovement.moveSpeed > targetSpeed)
        {
            while(playerMovement.moveSpeed > targetSpeed)
            {
                playerMovement.moveSpeed -= 0.001f;
                yield return new WaitForSeconds(Time.deltaTime / 2);
            }
            playerMovement.moveSpeed = targetSpeed;
        }
        else if (playerMovement.moveSpeed < targetSpeed)
        {
            while (playerMovement.moveSpeed < targetSpeed)
            {
                playerMovement.moveSpeed += 0.001f;
                yield return new WaitForSeconds(Time.deltaTime / 2);
            }
            playerMovement.moveSpeed = targetSpeed;
        }
    }

    IEnumerator StartLevel()
    {
        if (PlayerPrefs.GetFloat("X") != 0)
        {
            playerMovement.enabled = false;
            Debug.Log(PlayerPrefs.GetFloat("X") + " " + PlayerPrefs.GetFloat("Y") + " " + PlayerPrefs.GetFloat("Z"));
            transform.position = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
            yield return new WaitForSeconds(0.5f);
            playerMovement.enabled = true;
        }
    }
}

[CustomEditor(typeof(Life))]
class LifeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Reset PlayerPref"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
