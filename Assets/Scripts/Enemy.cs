using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public Slider healthBar;
    public SpriteRenderer spriteRenderer; // Ссылка на SpriteRenderer врага
    public Color damageColor = Color.red; // Цвет, который будет при попадании
    public float flashDuration = 0.2f;    // Длительность эффекта

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
        Destroy(gameObject);
    }
}
