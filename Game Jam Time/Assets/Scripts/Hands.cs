using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    //Weapon
    public GameObject heldItem;
    public ItemObject weaponType;

    public float PunchForce;

    //AttackRange
    public Collider AttackRange;
    public List<Collider> AtackRange = new List<Collider>();

    //Animations
    public Animator handsAnim;
    public Animator Legs;

    public void Update()
    {
        handsAnim.SetBool("Walk", Legs.GetBool("Walk"));
        handsAnim.SetBool("Run", Legs.GetBool("Run"));


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(weaponType != null && weaponType.type == ItemType.RangedWeapon)
            {
                RangedAttack(); 
                return;
            }
            else
            Attack();
        }
    }
    public void Attack()
    {
        WeaponObject weaponData = weaponType as WeaponObject;

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
                hitRigidbody.AddForce(forceDirection * PunchForce, ForceMode.Impulse);
            }
        }
    }

    public void RangedAttack()
    {
        RangedWeaponObject rangedWeaponData = weaponType as RangedWeaponObject;

        // Instantiate the bullet prefab at the position and rotation of the attacker
        GameObject bullet = Instantiate(rangedWeaponData.bullet, transform.position, transform.rotation);

            // Get the Rigidbody component of the bullet
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            // Add force to the bullet to make it move forward
            bulletRigidbody.AddForce(transform.forward * 5, ForceMode.Impulse);
        
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
