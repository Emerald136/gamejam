using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeGunsView : MonoBehaviour
{
    public static SwipeGunsView instance;

    [SerializeField] public GameObject[] Guns;
    
    void Awake() 
    {
        instance = this;
    } 

}
