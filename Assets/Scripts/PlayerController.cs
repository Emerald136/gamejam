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

    void Start() 
    {
        rb=GetComponent<Rigidbody2D>();
    }

    void Update() 
    {
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical")*moveSpeed;
        rb.velocity = new Vector2(speedX, speedY);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
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
    }
}
