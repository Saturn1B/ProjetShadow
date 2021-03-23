using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Audio;

public class Life : MonoBehaviour
{
    public float mentalHealthMax;
    public float currentMentalHealth;
    public List<GameObject> Light = new List<GameObject>();
    public List<GameObject> Follow = new List<GameObject>();
    public List<GameObject> Shadow = new List<GameObject>();
    float mentalGain = -0.02f;
    public Slider mentalHealthSlider;
    public GameObject CheckPoint;
    public PlayerMovement playerMovement;
    public Lighter lighter;
    public Camera camera;
    public bool isPaused;

    float targetSpeed;
    float FOV;

    public AudioSource audioSource;
    public AudioClip rising;
    public AudioClip falling;
    public float heartSpeed;

    private void Awake()
    {
        lighter = gameObject.GetComponent<Lighter>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        StartCoroutine(StartLevel());
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMentalHealth = mentalHealthMax;
        StartCoroutine(HeartBeat());
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
            playerMovement.moveFootSpeed = playerMovement.walkFootSpeed;
            FOV = 60;
        }
        else if (Light.Count <= 0 && Follow.Count > 0 && Shadow.Count <= 0)
        {
            mentalGain = -0.1f;
            targetSpeed = playerMovement.fearSpeed;
            playerMovement.moveFootSpeed = playerMovement.fearFootSpeed;
            FOV = 55;
        }
        else if (Light.Count <= 0 && Follow.Count <= 0 && Shadow.Count > 0)
        {
            mentalGain = -5f;
            targetSpeed = playerMovement.sprintSpeed;
            playerMovement.moveFootSpeed = playerMovement.sprintFootSpeed;
            FOV = 55;
        }
        else if (Light.Count > 0 && Follow.Count > 0 && Shadow.Count <= 0)
        {
            mentalGain = 0.1f;
            targetSpeed = playerMovement.fearSpeed;
            playerMovement.moveFootSpeed = playerMovement.fearFootSpeed;
            FOV = 60;
        }
        else if (Light.Count > 0 && Follow.Count <= 0 && Shadow.Count > 0)
        {
            mentalGain = 0.1f;
            targetSpeed = playerMovement.sprintSpeed;
            playerMovement.moveFootSpeed = playerMovement.sprintFootSpeed;
            FOV = 60;
        }
        else if (Light.Count <= 0 && Follow.Count > 0 && Shadow.Count > 0)
        {
            mentalGain = -5f;
            targetSpeed = playerMovement.sprintSpeed;
            playerMovement.moveFootSpeed = playerMovement.sprintFootSpeed;
            FOV = 55;
        }
        else if (Light.Count > 0 && Follow.Count > 0 && Shadow.Count > 0)
        {
            mentalGain = 0.1f;
            targetSpeed = playerMovement.walkSpeed;
            playerMovement.moveFootSpeed = playerMovement.walkFootSpeed;
            FOV = 60;
        }
        else if (Light.Count <= 0 && Follow.Count <= 0 && Shadow.Count <= 0)
        {
            mentalGain = -0.02f;
            targetSpeed = playerMovement.walkSpeed;
            playerMovement.moveFootSpeed = playerMovement.walkFootSpeed;
            FOV = 60;
        }
        #endregion

        if(camera.fieldOfView != FOV)
        {
            StartCoroutine(FOVChange(FOV));
        }

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
            PlayerPrefs.SetInt("Light", lighter.lightNumber);
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
                playerMovement.moveSpeed -= 0.002f;
                yield return new WaitForSeconds(Time.deltaTime / 2);
            }
            playerMovement.moveSpeed = targetSpeed;
        }
        else if (playerMovement.moveSpeed < targetSpeed)
        {
            while (playerMovement.moveSpeed < targetSpeed)
            {
                playerMovement.moveSpeed += 0.002f;
                yield return new WaitForSeconds(Time.deltaTime / 2);
            }
            playerMovement.moveSpeed = targetSpeed;
        }
    }

    IEnumerator FOVChange(float fOV)
    {
        if (camera.fieldOfView > fOV)
        {
            while (camera.fieldOfView > fOV)
            {
                camera.fieldOfView -= 0.001f;
                yield return new WaitForSeconds(Time.deltaTime / 2);
            }
            camera.fieldOfView = fOV;
        }
        else if (camera.fieldOfView < fOV)
        {
            while (camera.fieldOfView < fOV)
            {
                camera.fieldOfView += 0.001f;
                yield return new WaitForSeconds(Time.deltaTime / 2);
            }
            camera.fieldOfView = fOV;
        }
    }

    IEnumerator StartLevel()
    {
        if (PlayerPrefs.GetInt("Light") != 0)
        {
            lighter.lightNumber = PlayerPrefs.GetInt("Light");
        }

        if (PlayerPrefs.GetFloat("X") != 0)
        {
            playerMovement.enabled = false;
            Debug.Log(PlayerPrefs.GetFloat("X") + " " + PlayerPrefs.GetFloat("Y") + " " + PlayerPrefs.GetFloat("Z"));
            transform.position = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
            yield return new WaitForSeconds(0.5f);
            playerMovement.enabled = true;
        }
    }

    IEnumerator HeartBeat()
    {
        heartSpeed = Mathf.Clamp(mentalHealthSlider.value * 0.001f, 0.1f, 1);
        audioSource.Stop();
        audioSource.clip = rising;
        audioSource.Play();
        yield return new WaitForSeconds(heartSpeed);
        audioSource.Stop();
        audioSource.clip = falling;
        audioSource.Play();
        yield return new WaitForSeconds(heartSpeed*2);
        StartCoroutine(HeartBeat());
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
