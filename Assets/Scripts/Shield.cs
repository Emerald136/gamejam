using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GetComponentInParent<Enemy>().hasShield = false;
            Destroy(gameObject);
        }
    }
}
