using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed;
    public float fearSpeed;
    public float sprintSpeed;
    public float moveSpeed;

    Vector3 velocity;
    [SerializeField] private float gravity;

    public Transform groundCheck;
    public LayerMask groundMask;
    [SerializeField] private float groundDistance;

    [SerializeField] private float jumpHight;

    public ControlSettings control;

    private void Start()
    {
        moveSpeed = walkSpeed;
        control = GameObject.Find("ControlSettings").GetComponent<ControlSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        Gravity();
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
}
