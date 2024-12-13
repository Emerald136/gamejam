using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : GunBase
{
    public int pelletsCount = 5; // Количество дробин
    public float spreadAngle = 15f;

    public override void Shoot(Vector3 targetPosition)
    {
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + 1f / fireRate;

        for (int i = 0; i < pelletsCount; i++)
        {
            GameObject pellet = Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.identity);

            float angle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 direction = rotation * (targetPosition - muzzleTransform.position).normalized;

            pellet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }
}
