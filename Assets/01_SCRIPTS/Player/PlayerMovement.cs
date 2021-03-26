using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed;
    public float fearSpeed;
    public float sprintSpeed;
    public float moveSpeed;

    public float walkFootSpeed;
    public float fearFootSpeed;
    public float sprintFootSpeed;
    public float moveFootSpeed;

    Vector3 velocity;
    [SerializeField] private float gravity;

    public Transform groundCheck;
    public LayerMask groundMask;
    [SerializeField] private float groundDistance;

    [SerializeField] private float jumpHight;

    public ControlSettings control;

    public AudioClip[] foots;
    public AudioClip[] waterFoots;
    public AudioSource footSteps;
    bool isPressed = false;
    bool inShower;

    public AudioMixerSnapshot fadeIN;

    private void Start()
    {
        fadeIN.TransitionTo(4); 

        moveSpeed = walkSpeed;
        moveFootSpeed = walkFootSpeed;
        control = GameObject.Find("ControlSettings").GetComponent<ControlSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        Gravity();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void Gravity()
    {
        if (isGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void Movement()
    {
        float x = 0;

        if (control.Right.ReadValue<float>() > 0)
        {
            x = 1;
        }
        else if (control.Left.ReadValue<float>() > 0)
        {
            x = -1;
        }
        else
        {
            x = 0;
        }

        if(x != 0 && !isPressed)
        {
            StopAllCoroutines();
            StartCoroutine(Steps());
            isPressed = true;
        }
        else if(x == 0 && isPressed)
        {
            footSteps.Stop();
            StopAllCoroutines();
            isPressed = false;
        }

        if(x > 0)
        {
            transform.GetChild(0).transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (x < 0)
        {
            transform.GetChild(0).transform.eulerAngles = new Vector3(0, 180, 0);

        }

        Vector3 move = transform.right * x;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (control.Jump.triggered && isGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2 * gravity);
        }
    }

    private bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    IEnumerator InBetweenStep()
    {
        Debug.Log("steps");
        while (control.Right.ReadValue<float>() > 0 || control.Left.ReadValue<float>() > 0)
        {
            StartCoroutine(Steps());
            yield return new WaitForSeconds(moveFootSpeed);
        }
    }

    IEnumerator Steps()
    {
        if(control.Right.ReadValue<float>() > 0 || control.Left.ReadValue<float>() > 0)
        {
            footSteps.Stop();
            if (!inShower) { footSteps.clip = foots[Random.Range(0, foots.Length)]; }
            else { footSteps.clip = waterFoots[Random.Range(0, waterFoots.Length)]; }
            footSteps.Play();
        }
        yield return new WaitForSeconds(moveFootSpeed/2);
        if (control.Right.ReadValue<float>() > 0 || control.Left.ReadValue<float>() > 0)
        {
            footSteps.Stop();
            if (!inShower) { footSteps.clip = foots[Random.Range(0, foots.Length)]; }
            else { footSteps.clip = waterFoots[Random.Range(0, waterFoots.Length)]; }
            footSteps.Play();
        }
        yield return new WaitForSeconds(moveFootSpeed / 2);
        if (control.Right.ReadValue<float>() > 0 || control.Left.ReadValue<float>() > 0)
        {
            StartCoroutine(Steps());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shower"))
        {
            inShower = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shower"))
        {
            inShower = false;
        }
    }
}
