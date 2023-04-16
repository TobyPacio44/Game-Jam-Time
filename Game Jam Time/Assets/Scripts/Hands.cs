using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public Collider AttackRange;
    public List<Collider> AtackRange = new List<Collider>();

    public Animator handsAnim;
    public Animator Legs;
    public Movment movment;
    public float PuchForece;

    public void Update()
    {

        handsAnim.SetBool("Walk", Legs.GetBool("Walk"));
        handsAnim.SetBool("Run", Legs.GetBool("Run"));
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }
    public void Attack()
    {
        handsAnim.Play("Attack", 0, 0.0f);

        foreach (Collider collider in AtackRange)
        {
            // Get the Rigidbody component of the collider
            Rigidbody hitRigidbody = collider.GetComponent<Rigidbody>();

            // Add force to the collider if it has a Rigidbody component
            if (hitRigidbody != null)
            {
                Vector3 forceDirection = collider.transform.position - AttackRange.transform.position;
                forceDirection.Normalize();
                hitRigidbody.AddForce(forceDirection * PuchForece, ForceMode.Impulse);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PickUp") { return; }
        if (!AtackRange.Contains(other))
        {
            AtackRange.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (AtackRange.Contains(other))
        {
            AtackRange.Remove(other);
        }
    }
}
