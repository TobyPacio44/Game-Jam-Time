using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public float speed;
    public float normalSpeed;
    public float sprintSpeed;
    public float jumpForce;

    public Transform orientation;
    public Transform meshTransform;
    public GameObject groundCheck;
    private Rigidbody rb;

    public LayerMask whatIsGround;
    private float raycastDistance = 0.3f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = Physics.SphereCast(groundCheck.transform.position, 0.25f, Vector3.down, out RaycastHit hit, raycastDistance, whatIsGround);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : normalSpeed;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
      
        Vector3 movement = new Vector3(x, 0.0f, y);

        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);

        if (movement.magnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            meshTransform.rotation = Quaternion.Slerp(meshTransform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}