using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType { Slow, Back, Kill };
    public BulletType bulletType;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            Debug.Log("BulletOnEnemy");
            switch (bulletType)
            {
                case BulletType.Slow:
                    SlowEnemy(collision.collider);
                    break;
                case BulletType.Back:
                    BackEnemy(collision.collider);
                    break;
                case BulletType.Kill:
                    KillEnemy(collision.collider);
                    break;
                default:
                    break;
            }
        }
        Destroy(gameObject);
    }

    void SlowEnemy(Collider enemy)
    {
        // Slow down the enemy by the slowValue amount
        enemy.GetComponent<Enemy>().SlowDown(2, 10);
    }

    void BackEnemy(Collider enemy)
    {
        // Send the enemy back a certain distance
        enemy.transform.position -= enemy.transform.forward * 2f;
    }

    void KillEnemy(Collider enemy)
    {
        // Kill the enemy
        enemy.GetComponent<Enemy>().Die();
    }
}
