using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    public float fireRate;
    public GameObject bulletPrefab;
    public Transform muzzleTransform;
    public float bulletSpeed;

    protected float nextFireTime;

    public abstract void Shoot(Vector3 targetPosition);
}
