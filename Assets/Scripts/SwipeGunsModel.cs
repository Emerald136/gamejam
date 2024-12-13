using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeGunsModel : MonoBehaviour
{
    public static SwipeGunsModel instance;
    public GunBase currentGunInstance;

    public string currentGun = "";
    public Sprite currentSprite;
    
    void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        SwipeGunsPresenter.instance.SwipeGun(PlayerPrefs.GetString("currentGame"));
    }

    public void SaveCurrentGun() 
    {
        PlayerPrefs.SetString("currentGame", currentGun);
        PlayerPrefs.Save();
    }
}
