using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public float speed = 3.5f;

    public bool slowing, backing;

    // Other variables and functions related to the Enemy script

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (agent.enabled)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void SlowDown(float amount, float duration)
    {
        if (!slowing)
        {
            agent.speed -= amount;
            StartCoroutine(SlowDownCoroutine(amount, duration));
            slowing = true;
        }
    }

    IEnumerator SlowDownCoroutine(float amount, float duration)
    {
        yield return new WaitForSeconds(duration);
        slowing = false;
        agent.speed += amount;
    }
}
