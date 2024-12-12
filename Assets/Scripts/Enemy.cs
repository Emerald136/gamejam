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
    public bool hasShield = false; // Указывает, есть ли щит
    public GameObject shieldPrefab;           // Префаб щита
    private GameObject currentShieldInstance; // Ссылка на текущий щит

    private ScoreManager scoreManager;    
    public Animator animator;

    public BombEnemy bombEnemyScript;
    public EnemyController enemyController;

    private void Start()
    {
        if(healthBar!=null)
        {
            healthBar.maxValue = 100;
            healthBar.value = health;
        }
        

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
        if(healthBar!=null) healthBar.value = health;
        StartCoroutine(FlashDamage());

        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public void ApplyShield()
    {
        if (!hasShield && shieldPrefab != null)
        {
            hasShield = true;
            // Спавн щита
            currentShieldInstance = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
        }
    }

    public void RemoveShield()
    {
        if (hasShield)
        {
            hasShield = false;
            // Уничтожаем щит
            if (currentShieldInstance != null)
            {
                Destroy(currentShieldInstance);
            }
        }
    }

    IEnumerator FlashDamage()
    {
        // Сохраняем исходный цвет
        Color originalColor = Color.white;

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

    IEnumerator Die()
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

        if(bombEnemyScript != null) bombEnemyScript.enabled = false;
        if(enemyController != null) enemyController.enabled = false;

        bool isAnim = false;
        try
        {
            animator.GetBool("Die");
            animator.SetBool("Die", true);
            isAnim = true;
        }
        catch
        {
            isAnim = false;
        }
        if(isAnim) yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
