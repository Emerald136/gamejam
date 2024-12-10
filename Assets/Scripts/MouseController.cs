using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Transform weaponHolder;
    public Transform characterSprite;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - transform.position;
        direction.z = 0;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        weaponHolder.rotation = Quaternion.Euler(0, 0, angle);

        if (direction.x > 0) 
        {
            characterSprite.localScale = new Vector3(5, 5, 5);
        }
        else if (direction.x < 0) 
        {
            characterSprite.localScale = new Vector3(-5, 5, 5);
        }
    }
}
