using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    
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
    }
}
