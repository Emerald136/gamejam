using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeGunsModel : MonoBehaviour
{
    public static SwipeGunsModel instance;

    public string currentGun = "";
    public Sprite currentSprite;
    
    void Awake() 
    {
        instance = this;
    }
}