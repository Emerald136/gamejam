using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ProneWeapon : MonoBehaviour
{
    [SerializeField] private GameObject textClickE;
    private bool isGun;
    void Start() 
    {
        SpriteRenderer gun = GetComponent<SpriteRenderer>();
        gameObject.name = gun.sprite.name;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            textClickE.SetActive(true);
            isGun = true;
            StartCoroutine(CheckInputCoroutine());
        }
    }

    private IEnumerator CheckInputCoroutine()
    {
        while (isGun)
        {
            if (!isGun) 
            {
                StopAllCoroutines();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (SwipeGunsModel.instance.currentGun == "")
                {
                    SwipeGunsPresenter.instance.SwipeGun(gameObject.name);
                    Destroy(gameObject);
                }
                else
                {
                    string currentObject = gameObject.name;
                    SpriteRenderer gun = GetComponent<SpriteRenderer>();
                    if (SwipeGunsModel.instance.currentSprite != null)
                    {
                        gun.sprite = SwipeGunsModel.instance.currentSprite;
                        gameObject.name = SwipeGunsModel.instance.currentGun;
                        SwipeGunsPresenter.instance.SwipeGun(currentObject);
                    }
                }

                yield break;
            }
            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        textClickE.SetActive(false);
        isGun = false;
        StopAllCoroutines();
    }
}
