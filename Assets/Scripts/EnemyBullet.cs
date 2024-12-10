using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;      // Скорость пули
    public float lifetime = 5f;   // Время жизни пули

    void Start()
    {
        Destroy(gameObject, lifetime); // Уничтожаем пулю через заданное время
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Логика повреждения игрока
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(10); // Например, 10 единиц урона
            }
            Destroy(gameObject); // Уничтожаем пулю
        }
    }
}
