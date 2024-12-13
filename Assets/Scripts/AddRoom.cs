using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    [Header("Wall")]
    public GameObject[] walls;
    public GameObject wallEffect;
    public GameObject door;

    [Header("Enemies")]
    public GameObject[] enemyTypes;
    public Transform[] enemySpawners;

    [Header("РЎРЅРёР·Сѓ РµС‰Рµ Р±РѕРЅСѓСЃС‹ РјРѕР¶РЅРѕ РґРѕР±Р°РІРёС‚СЊ (Powerups)")]

    [HideInInspector] public List<GameObject> enemies;
    public GameObject[] variantsRoom;
    private bool spawned;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !spawned) 
        {
            spawned = true;

            foreach(Transform spawner in enemySpawners) 
            {
                int rand = Random.Range(0, 11);
                if (rand < 9) 
                {
                    GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                    GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                    enemy.transform.parent = transform;
                    enemies.Add(enemy);
                }
                // РґР°Р»СЊС€Рµ РјРѕР¶РЅРѕ СЃРїР°РІРЅРёС‚СЊ Р±РѕРЅСѓСЃС‹ РґРѕРїСѓСЃС‚РёРј СЃ РјРµРЅСЊС€РµР№ РІРµСЂРѕСЏС‚РЅРѕСЃС‚СЊСЋ
                Debug.Log(enemies.Count);
            }
            StartCoroutine(CheckEnemies());
        }
    }

    IEnumerator CheckEnemies() 
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyWalls();
    }
    public void DestroyWalls() 
    {
        Debug.Log("DestroyWalls");
        foreach (GameObject wall in walls)
{
    if (wall != null) 
    {
        Debug.Log("DestroyWalls2");
        Instantiate(wallEffect, wall.transform.position, Quaternion.identity);
        Destroy(wall);
    }
    else
    {
        Debug.LogWarning("Один из объектов стены равен null!");
    }
}
    }
}
