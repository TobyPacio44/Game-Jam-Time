using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    //ten skrypt jest prototypem i mo¿na go póŸniej dowolnie zmieniaæ
    public float Speed;
    [SerializeField]
    Rigidbody rb;
    public Transform PlayerMesh;
    public Transform Cam;

    public void FixedUpdate()
    {
        Vector3 camPos = Cam.position;
        camPos.x = transform.position.x;
        Cam.position = camPos;
        Cam.LookAt(camPos);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDir = Cam.transform.forward * vertical + Cam.transform.right * horizontal;
        rb.velocity = moveDir.normalized * Speed;
        if (rb.velocity != Vector3.zero)
        {
            PlayerMesh.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }
}