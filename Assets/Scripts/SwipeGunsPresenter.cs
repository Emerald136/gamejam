using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeGunsPresenter : MonoBehaviour
{
    public static SwipeGunsPresenter instance;
    [SerializeField] public Sprite[] spriteGuns;
    [SerializeField] private Sprite specificSprite;

    void Awake() 
    {
        instance = this;
    }

    void Start() 
    {
        spriteGuns = Resources.LoadAll<Sprite>("Guns");
    }

    public void SwipeGun(string gun) 
    {
        SpecificSprite(gun);
        SwipeGunsModel.instance.currentSprite = specificSprite;
        GunsDontView();
        switch (gun) 
        {
            case "Pistols":
                SwipeGunsModel.instance.currentGun = Guns.Pistols;
                ShowGun(Guns.Pistols);
                break;
            case "Knife":
                SwipeGunsModel.instance.currentGun = Guns.Knife;
                ShowGun(Guns.Knife);
                break;
        }
    }
    
    public void SpecificSprite(string gun) 
    {
        for (int i = 0; i < spriteGuns.Length; i++) 
        {
            if (gun == spriteGuns[i].name) 
            {
                specificSprite = spriteGuns[i];
            }
        }
    }

    public void ShowGun(string gun) 
    {
        for (int i = 0; i < SwipeGunsView.instance.Guns.Length; i++) 
        {
            if (SwipeGunsView.instance.Guns[i].name == gun) 
            {
                SwipeGunsView.instance.Guns[i].SetActive(true);
            }
        }
    }

    private void GunsDontView() 
    {
        for (int i = 0; i < SwipeGunsView.instance.Guns.Length; i++) 
        {
            SwipeGunsView.instance.Guns[i].SetActive(false);
        }
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            SwipeGun(Guns.Pistols);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwipeGun(Guns.Knife);
        }
    }
}
