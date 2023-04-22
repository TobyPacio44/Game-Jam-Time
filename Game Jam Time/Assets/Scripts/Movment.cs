using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float normalSpeed;
    [HideInInspector] public float sprintSpeed;
    [HideInInspector] public float crouchSpeed;

    [HideInInspector] public float jumpForce;

    [HideInInspector] public Transform orientation;
    [HideInInspector] public GameObject groundCheck;
    [HideInInspector] public float IsStanding;
    private Rigidbody rb;

    public CapsuleCollider capsuleCollider;
    public Vector3 center;
    public DetectUp crouchingDetect;
    public GameObject Mesh;

    public LayerMask whatIsGround;
    private float raycastDistance = 0.3f;
    private bool isGrounded;
    public float CrouchDistance;
    public bool crouching;

    public Animator LegsAnimator;
    public ParticleSystem JumpInpact;
  
    void Start()
    {
        capsuleCollider =  GetComponent<CapsuleCollider>();
        orientation = transform.Find("Mesh");
        groundCheck = transform.Find("GroundCheck").gameObject;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = Physics.SphereCast(groundCheck.transform.position, 0.25f, Vector3.down, out RaycastHit hit, raycastDistance, whatIsGround);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            LegsAnimator.Play("Jump",0,0.0f);
            JumpInpact.Play();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            center = capsuleCollider.center;
            center.y = -0.46f;
            capsuleCollider.center = center;
            capsuleCollider.height = 1.789756f;
            crouching = true;
            Mesh.transform.localPosition = new Vector3(0, CrouchDistance, 0);
            
        }
        else if(!crouchingDetect.isColliding)
        {
            center.y = -0.0974462f;
            capsuleCollider.center = center;
            capsuleCollider.height = 2.514893f;
            crouching = false;
            Mesh.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        speed = (Input.GetKey(KeyCode.LeftShift) && !crouching) ? sprintSpeed : crouching ? crouchSpeed : normalSpeed;
        LegsAnimator.SetBool("Run", Input.GetKey(KeyCode.LeftShift));
        LegsAnimator.SetBool("Kuc", crouching);
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
            orientation.rotation = Quaternion.Slerp(orientation.rotation, targetRotation, Time.deltaTime * 10f);
            LegsAnimator.SetBool("Walk", true);
            IsStanding = movement.magnitude + 0.1f;
        }
        else
        {
            LegsAnimator.SetBool("Walk", false);
            IsStanding = movement.magnitude + 0.35f;
        }
    }
}