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
        // GunsDontView();
        // switch (gun) 
        // {
        //     case "Pistols":
        //         SwipeGunsModel.instance.currentGun = Guns.Pistols;
        //         ShowGun(Guns.Pistols);
        //         break;
        //     case "Pistolet":
        //         SwipeGunsModel.instance.currentGun = Guns.Pistolet;
        //         ShowGun(Guns.Pistolet);
        //         break;
        //     case "Drobash":
        //         SwipeGunsModel.instance.currentGun = Guns.Drobash;
        //         ShowGun(Guns.Drobash);
        //         break;
        // }
        GunsDontView();
        for (int i = 0; i < SwipeGunsView.instance.Guns.Length; i++)
        {
            if (SwipeGunsView.instance.Guns[i].name == gun)
            {
                SwipeGunsView.instance.Guns[i].SetActive(true);
                SwipeGunsModel.instance.currentGunInstance = SwipeGunsView.instance.Guns[i].GetComponent<GunBase>();
                print(SwipeGunsView.instance.Guns[i].GetComponent<GunBase>());
            }
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
}
