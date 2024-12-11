using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;          // Скорость движения врага
    public float dodgeSpeed = 5f;        // Скорость уклонения
    public float fireRate = 1f;          // Частота стрельбы
    public GameObject bulletPrefab;      // Префаб пули врага
    public Transform firePoint;          // Точка стрельбы
    public LayerMask bulletLayer;        // Слой пуль
    public float detectionRadius = 3f;   // Радиус уклонения от пуль
    public float optimalDistance = 5f;   // Оптимальная дистанция от игрока
    private AudioSource audioSource;     // Компонент AudioSource
    public AudioClip shootSound;         // Звук выстрела
    private Transform player;            // Ссылка на игрока
    private Rigidbody2D rb;
    private float lastFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Игрок с тегом 'Player' не найден в сцене!");
        }
        audioSource = GetComponent<AudioSource>(); // Инициализация компонента AudioSource
    }

    void Update()
    {
        if (player == null) return;

        AimAndShoot();
        MoveTowardsPlayer();
        DodgeBullets();
    }

    void AimAndShoot()
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

    void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > optimalDistance)
        {
            // Если враг слишком далеко, он приближается к игроку
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            // Если враг на оптимальной дистанции, он остается на месте
            rb.velocity = Vector2.zero;
        }
    }

    void DodgeBullets()
    {
        // Проверяем наличие пуль в радиусе
        Collider2D[] bullets = Physics2D.OverlapCircleAll(transform.position, detectionRadius, bulletLayer);

        foreach (var bullet in bullets)
        {
            // Получаем Rigidbody2D пули
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                // Траектория движения пули
                Vector2 bulletDirection = bulletRb.velocity.normalized;

                // Определяем направление уклонения
                Vector2 toEnemy = (transform.position - bullet.transform.position).normalized;

                // Выбираем направление уклонения, перпендикулярное траектории пули
                Vector2 dodgeDirection = Vector2.Perpendicular(bulletDirection);

                // Проверяем, в какую сторону уклоняться: влево или вправо
                if (Vector2.Dot(dodgeDirection, toEnemy) < 0)
                {
                    dodgeDirection = -dodgeDirection; // Меняем направление, если нужно
                }

                // Применяем уклонение
                rb.velocity = dodgeDirection * dodgeSpeed;
                return; // Уклоняемся от одной пули за раз
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация радиуса уклонения
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
