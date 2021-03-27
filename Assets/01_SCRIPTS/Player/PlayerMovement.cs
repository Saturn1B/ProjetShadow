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
    public Animator animator;

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
            animator.SetBool("Moving", true);
        }
        else if (control.Left.ReadValue<float>() > 0)
        {
            x = -1;
            animator.SetBool("Moving", true);
        }
        else
        {
            x = 0;
            animator.SetBool("Moving", false);
        }

        if (x != 0 && !isPressed)
        {
            StopAllCoroutines();
            StartCoroutine(Steps());
            StartCoroutine(JumpEnd());
            isPressed = true;
        }
        else if(x == 0 && isPressed)
        {
            footSteps.Stop();
            StopAllCoroutines();
            StartCoroutine(JumpEnd());
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
            animator.SetBool("Jumping", true);
            StartCoroutine(JumpEnd());
        }
    }

    private bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
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

    IEnumerator JumpEnd()
    {
        yield return new WaitForSeconds(0.3f);
        Debug.Log("connard");
        animator.SetBool("Jumping", false);
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
