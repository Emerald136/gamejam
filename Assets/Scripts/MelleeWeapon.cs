using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleeWeapon : MonoBehaviour
{
    public int damage = 20; // Урон оружия
    public bool isAttacking = false; // Флаг атаки

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
