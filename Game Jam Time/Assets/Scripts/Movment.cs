using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public float speed;
    public float normalSpeed;
    public float sprintSpeed;
    public Transform orientation;
    public Transform meshTransform;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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