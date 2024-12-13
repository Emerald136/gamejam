using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    public float moveSpeed = 3f;          // Скорость движения врага
    public float explosionRadius = 2f;    // Радиус взрыва
    public float explosionDelay = 1f;     // Задержка перед взрывом
    public int damage = 50;               // Урон от взрыва
    public LayerMask playerLayer;         // Слой игрока
    public GameObject explosionEffect;    // Эффект взрыва (частицы)

    private Transform player;             // Ссылка на игрока
    private bool isExploding = false;     // Флаг, сигнализирующий о начале взрыва
    public Animator animator; // Ссылка на Animator
    private AddRoom room;

    void Start()
    {
        room = GetComponentInParent<AddRoom>();
        player = GameObject.FindWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Игрок с тегом 'Player' не найден в сцене!");
        }
    }

    void Update()
    {
        if (player == null || isExploding) return;

        MoveTowardsPlayer();

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= explosionRadius)
        {
            StartCoroutine(Explode());
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    System.Collections.IEnumerator Explode()
    {
        isExploding = true;
        yield return new WaitForSeconds(explosionDelay);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, playerLayer);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<PlayerStats>()?.TakeDamage(damage);
            }
        }
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        room.enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
