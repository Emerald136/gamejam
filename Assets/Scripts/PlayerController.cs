using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    public GameObject bulletPrefabDrobash;
    public GameObject bulletPrefabPistols;
    public GameObject bulletPrefabPistolet;
    public float ballSpeed = 10f;
    public Transform muzzleTransform;
    private AudioSource audioSource;
    public Animator animator;
    private GameObject bulletInst;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // if (FrozenPuddle.instance.isFrozenPuddle == false && TrainPresenter.instance.isMovingTrain == false)
        // {
            speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
            speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
            rb.velocity = new Vector2(speedX, speedY);
            float speed = rb.velocity.magnitude;

            animator.SetBool("isMoving", speed > 0.1f);

            if (Input.GetMouseButtonDown(0) && SwipeGunsModel.instance.currentGunInstance != null)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;

                if (SwipeGunsModel.instance.currentGunInstance is RifleGun rifleGun)
                {
                    rifleGun.StartFiring(mousePosition);
                }
                else
                {
                    SwipeGunsModel.instance.currentGunInstance.Shoot(mousePosition);
                }
            }

            if (Input.GetMouseButtonUp(0) && SwipeGunsModel.instance.currentGunInstance is RifleGun rifle)
            {
                rifle.StopFiring();
            }
        // }
    }

    void Shoot(string currentGun)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        switch (currentGun) 
        {
            case "Pistols":
                bulletInst = bulletPrefabPistols;
                break;
            case "Pistolet":
                bulletInst = bulletPrefabPistolet;
                break;
            case "Drobash":
                bulletInst = bulletPrefabDrobash;
                break;
        }
        GameObject ball = Instantiate(bulletInst, muzzleTransform.position, Quaternion.identity);
        Vector3 direction = (mousePosition - transform.position).normalized;
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = direction * ballSpeed;

        if (audioSource != null)
        {
            audioSource.Play(); 
        }
    }
}
