using UnityEngine;

public class SnowController : MonoBehaviour
{
    public ParticleSystem snowParticles; // Ссылка на систему частиц
    private ParticleSystem.EmissionModule emissionModule; // Эмиссия частиц

    private float baseEmissionRate = 50f; // Стандартное количество частиц
    private float heavyEmissionRate = 20000000f; // Усиленное количество частиц

    private float heavySnowDuration = 15f; // Длительность усиленного снегопада
    private float nextSnowIncreaseTime = 0f; // Время следующего усиления снегопада

    void Start()
    {
        // Получаем модуль эмиссии системы частиц
        if (snowParticles != null)
        {
            emissionModule = snowParticles.emission;
            emissionModule.rateOverTime = baseEmissionRate; // Устанавливаем стандартное значение
        }

        ScheduleNextSnowIncrease(); // Планируем первое усиление
    }

    void Update()
    {
        if (Time.time >= nextSnowIncreaseTime)
        {
            StartCoroutine(HeavySnowCoroutine());
        }
    }

    private void ScheduleNextSnowIncrease()
    {
        // Устанавливаем случайный промежуток времени для следующего усиления
        nextSnowIncreaseTime = Time.time + Random.Range(10f, 30f); // Интервал от 10 до 30 секунд
    }

    private System.Collections.IEnumerator HeavySnowCoroutine()
    {
        // Увеличиваем снегопад
        emissionModule.rateOverTime = heavyEmissionRate;

        // Ждём указанное время
        yield return new WaitForSeconds(heavySnowDuration);

        // Возвращаем стандартный снегопад
        emissionModule.rateOverTime = baseEmissionRate;

        // Планируем следующее усиление
        ScheduleNextSnowIncrease();
    }
}
