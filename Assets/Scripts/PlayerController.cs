using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    public GameObject ballPrefab;
    public float ballSpeed = 10f;
    public Transform muzzleTransform;
    private AudioSource audioSource;
    public Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (FrozenPuddle.instance.isFrozenPuddle == false && TrainPresenter.instance.isMovingTrain == false)
        {
            speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
            speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
            rb.velocity = new Vector2(speedX, speedY);
            float speed = rb.velocity.magnitude;

            animator.SetBool("isMoving", speed > 0.1f);

            if (SwipeGunsModel.instance.currentGun != "" && Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        GameObject ball = Instantiate(ballPrefab, muzzleTransform.position, Quaternion.identity);
        Vector3 direction = (mousePosition - transform.position).normalized;
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = direction * ballSpeed;

        
        if (audioSource != null)
        {
            audioSource.Play(); 
        }
    }
}
