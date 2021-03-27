using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class End : MonoBehaviour
{
    public GameObject HUD, EndCredit;
    public Animator animator;
    public AudioMixer Sound;
    bool audio = false;
    float value;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HUD.SetActive(false);
            EndCredit.SetActive(true);
            animator.SetBool("Moving", false);
            animator.SetBool("Jumping", false);
            audio = Sound.GetFloat("SoundParam", out value);
            Sound.SetFloat("SoundParam", -80);
            other.gameObject.GetComponent<PlayerMovement>().enabled = false;
            other.gameObject.GetComponent<Life>().enabled = false;
            other.gameObject.GetComponent<Lighter>().enabled = false;
            StartCoroutine(MoveCredit());
        }
    }

    IEnumerator MoveCredit()
    {
        while(EndCredit.transform.position.y < 2000)
        {
            EndCredit.transform.position += new Vector3(0, 0.5f, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Sound.SetFloat("SoundParam", value);
        SceneManager.LoadScene(0);
    }
}
