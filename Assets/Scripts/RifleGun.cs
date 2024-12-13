using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleGun : GunBase
{
    private bool isFiring;

    public override void Shoot(Vector3 targetPosition)
    {
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + 1f / fireRate;
        GameObject bullet = Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.identity);
        Vector3 direction = (targetPosition - muzzleTransform.position).normalized;
        // Поворачиваем пулю в направлении её движения
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    public void StartFiring(Vector3 targetPosition)
    {
        isFiring = true;
        StartCoroutine(AutoFire(targetPosition));
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    private IEnumerator AutoFire(Vector3 targetPosition)
    {
        while (isFiring)
        {
            Shoot(targetPosition);
            yield return new WaitForSeconds(1f / fireRate);
        }
    }
}
