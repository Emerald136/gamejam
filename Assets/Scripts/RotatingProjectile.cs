using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingProjectile : MonoBehaviour
{
    public float speed = 10f;             // Скорость полета
    public float rotationSpeed = 300f;   // Скорость вращения
    public float lifeTime = 5f;          // Время жизни пули

    private Vector2 direction;           // Направление движения
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Устанавливаем направление движения (вправо относительно начального направления)
        direction = transform.right;

        // Уничтожаем пулю через заданное время
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Визуальное вращение снаряда
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Обеспечиваем постоянное движение в одном направлении
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Логика повреждения игрока
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(10);
            }
            Destroy(gameObject);
        }
    }
}
