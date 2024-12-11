using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public Slider healthBar;
    public SpriteRenderer spriteRenderer; // Ссылка на SpriteRenderer врага
    public Color damageColor = Color.red; // Цвет, который будет при попадании
    public float flashDuration = 0.2f;    // Длительность эффекта
    public int scoreValue = 5;            // Очки за убийство (по умолчанию 5)

    private ScoreManager scoreManager;    

    private void Start()
    {
        healthBar.maxValue = 100;
        healthBar.value = health;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer не найден!");
            }
        }
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;
        StartCoroutine(FlashDamage());

        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashDamage()
    {
        // Сохраняем исходный цвет
        Color originalColor = spriteRenderer.color;

        float elapsedTime = 0f;
        float halfDuration = flashDuration / 2f;

        // Плавно меняем цвет на damageColor
        while (elapsedTime < halfDuration)
        {
            spriteRenderer.color = Color.Lerp(originalColor, damageColor, elapsedTime / halfDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        // Плавно возвращаем цвет к оригинальному
        while (elapsedTime < halfDuration)
        {
            spriteRenderer.color = Color.Lerp(damageColor, originalColor, elapsedTime / halfDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = originalColor;
    }

    void Die()
    {
        // Проверяем, является ли этот враг с бомбой
        BombEnemy bombEnemy = GetComponent<BombEnemy>();
        if (bombEnemy != null)
        {
            // Если это враг с бомбой, даем 10 очков
            scoreValue = 10;
        }
        if (scoreManager != null)
        {
            scoreManager.AddScore(scoreValue);
        }
        Destroy(gameObject);
    }
}
