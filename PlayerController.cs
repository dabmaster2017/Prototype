using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpHeight = 2.4f;
    public float gravity = -15f;
    public Transform ground;
    public float distanceToGround = 0.4f;
    public LayerMask groundMask;
    private CharacterInput controls;
    private CharacterController controller;
    private int jumpAllowance = 1;
    private Vector2 move;
    private Vector3 velocity;

    private void Awake()
    {
        controls = new CharacterInput();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Grav();
        Jump();

        if (transform.position.x > 6)
        {
            transform.position = new Vector3(6, transform.position.y, transform.position.z);
        }

        if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }

    private void PlayerMovement()
    {
        move = controls.Player.Movement.ReadValue<Vector2>();
        var movement = move.y * transform.forward + move.x * transform.right;
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(ground.position, distanceToGround, groundMask);
    }

    private void Grav()
    {
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (controls.Player.Jump.triggered && jumpAllowance > 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (IsGrounded())
        {
            jumpAllowance = 1;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}