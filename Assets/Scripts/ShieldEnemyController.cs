using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyController : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint;     // Точка стрельбы
    public float fireRate = 1f;     // Скорость стрельбы (выстрелы в секунду)
    public float shieldCooldown = 5f; // Перезарядка наложения щита
    public float detectionRadius = 5f; // Радиус обнаружения игрока
    public LayerMask playerLayer;   // Слой игрока
    public LayerMask enemyLayer;    // Слой врагов
    public float moveSpeed = 3f;    // Скорость перемещения врага
    public float minDistanceFromPlayer = 2f; // Минимальное расстояние от игрока
    public float maxDistanceFromPlayer = 5f; // Максимальное расстояние от игрока

    private float nextFireTime = 0f;
    private float nextShieldTime = 0f;
    private Transform player;  
    private float lastFireTime = 0f;
    private AudioSource audioSource;     // Компонент AudioSource
    public AudioClip shootSound;         // Звук выстрела

    void Update()
    {
        // Стрельба по игроку
        DetectAndShootPlayer();

        // Наложение щита на союзников
        ShieldAllies();

        // Перемещение врага
        MoveEnemy();
    }

    void DetectAndShootPlayer()
    {
        // Проверяем наличие игрока в радиусе
        player = GameObject.FindWithTag("Player").transform;
        if (player != null)
        {
            // Направление на игрока
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0, 0, angle);

            // Стрельба
            if (Time.time >= lastFireTime + 1f / fireRate)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                lastFireTime = Time.time;

                // Проигрываем звук выстрела
                if (audioSource != null && shootSound != null)
                {
                    audioSource.PlayOneShot(shootSound); // Воспроизведение звука
                }
            }
        }
    }

    void ShieldAllies()
    {
        if (Time.time >= nextShieldTime)
        {
            // Найдём всех союзников в радиусе
            Collider2D[] allies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
            foreach (var ally in allies)
            {
                Enemy enemyComponent = ally.GetComponent<Enemy>();
                if (enemyComponent != null && !enemyComponent.hasShield)
                {
                    ApplyShield(enemyComponent);
                    break; // Накладываем щит только на одного
                }
            }
            nextShieldTime = Time.time + shieldCooldown;
        }
    }

    void ApplyShield(Enemy ally)
    {
        ally.ApplyShield(); // Активируем щит через метод Enemy
        // Можно добавить визуальный эффект щита
        Debug.Log($"Щит наложен на {ally.name}");
    }

    void MoveEnemy()
    {
        if (player != null)
        {
            // Получаем расстояние до игрока
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Если враг слишком близко, двигаемся в случайную точку на подходящем расстоянии от игрока
            if (distanceToPlayer < minDistanceFromPlayer)
            {
                MoveAwayFromPlayer();
            }
            // Если враг слишком далеко, двигаемся к игроку, но не подходим слишком близко
            else if (distanceToPlayer > maxDistanceFromPlayer)
            {
                MoveTowardsPlayer();
            }
            else
            {
                // Враг держится на дистанции между minDistanceFromPlayer и maxDistanceFromPlayer
                // Можно добавить случайное движение в этом радиусе
                MoveRandomlyAroundPlayer();
            }
        }
    }

    void MoveAwayFromPlayer()
    {
        // Двигаем врага от игрока
        Vector2 direction = (transform.position - player.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void MoveTowardsPlayer()
    {
        // Двигаем врага к игроку
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void MoveRandomlyAroundPlayer()
    {
        // Двигаем врага в случайном направлении вокруг игрока в пределах заданного диапазона
        Vector2 randomDirection = Random.insideUnitCircle * (maxDistanceFromPlayer - minDistanceFromPlayer);
        Vector2 newPosition = (Vector2)player.position + randomDirection;
        Vector2 direction = (newPosition - (Vector2)transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnDrawGizmosSelected()
    {
        // Отображаем радиус обнаружения и следования в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDistanceFromPlayer); // Минимальное расстояние от игрока
        Gizmos.DrawWireSphere(transform.position, maxDistanceFromPlayer); // Максимальное расстояние от игрока
    }
}
