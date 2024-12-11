using UnityEngine;

public class SnowController : MonoBehaviour
{
    public ParticleSystem snowParticles; // ������ �� ������� ������
    private ParticleSystem.EmissionModule emissionModule; // ������� ������

    private float baseEmissionRate = 50f; // ����������� ���������� ������
    private float heavyEmissionRate = 20000000f; // ��������� ���������� ������

    private float heavySnowDuration = 15f; // ������������ ���������� ���������
    private float nextSnowIncreaseTime = 0f; // ����� ���������� �������� ���������

    void Start()
    {
        // �������� ������ ������� ������� ������
        if (snowParticles != null)
        {
            emissionModule = snowParticles.emission;
            emissionModule.rateOverTime = baseEmissionRate; // ������������� ����������� ��������
        }

        ScheduleNextSnowIncrease(); // ��������� ������ ��������
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
        // ������������� ��������� ���������� ������� ��� ���������� ��������
        nextSnowIncreaseTime = Time.time + Random.Range(10f, 30f); // �������� �� 10 �� 30 ������
    }

    private System.Collections.IEnumerator HeavySnowCoroutine()
    {
        // ����������� ��������
        emissionModule.rateOverTime = heavyEmissionRate;

        // ��� ��������� �����
        yield return new WaitForSeconds(heavySnowDuration);

        // ���������� ����������� ��������
        emissionModule.rateOverTime = baseEmissionRate;

        // ��������� ��������� ��������
        ScheduleNextSnowIncrease();
    }
}
