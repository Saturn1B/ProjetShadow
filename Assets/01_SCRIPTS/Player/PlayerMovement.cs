using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField] private float walkSpeed;
    float moveSpeed;

    Vector3 velocity;
    [SerializeField] private float gravity;

    public Transform groundCheck;
    public LayerMask groundMask;
    [SerializeField] private float groundDistance;

    [SerializeField] private float jumpHight;

    private void Start()
    {
        moveSpeed = walkSpeed;
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
        float x = Input.GetAxis("Horizontal");

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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2 * gravity);
        }
    }

    private bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
