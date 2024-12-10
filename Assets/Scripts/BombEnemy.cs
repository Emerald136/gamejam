using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    public float moveSpeed = 3f;          // Скорость движения врага
    public float explosionRadius = 2f;   // Радиус взрыва
    public float explosionDelay = 1f;    // Задержка перед взрывом
    public int damage = 50;           // Урон от взрыва
    public LayerMask playerLayer;        // Слой игрока
    public GameObject explosionEffect;   // Эффект взрыва (частицы)
    
    private Transform player;            // Ссылка на игрока
    private bool isExploding = false;    // Флаг, сигнализирующий о начале взрыва

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Игрок с тегом 'Player' не найден в сцене!");
        }
    }

    void Update()
    {
        if (player == null || isExploding) return;

        // Движение к игроку
        MoveTowardsPlayer();

        // Проверка на близость к игроку
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= explosionRadius)
        {
            StartCoroutine(Explode());
        }
    }

    void MoveTowardsPlayer()
    {
        // Движение врага в направлении игрока
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    System.Collections.IEnumerator Explode()
    {
        isExploding = true; // Запускаем процесс взрыва

        // Ожидание перед взрывом
        yield return new WaitForSeconds(explosionDelay);

        // Проверяем, есть ли игрок в радиусе взрыва
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, playerLayer);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                // Наносим урон игроку (вы должны реализовать метод TakeDamage)
                hit.GetComponent<PlayerStats>()?.TakeDamage(damage);
            }
        }

        // Показываем эффект взрыва
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Уничтожаем врага
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса взрыва
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
