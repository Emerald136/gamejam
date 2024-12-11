using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrainModel : MonoBehaviour
{
    public static TrainModel instance;
    public int countdownTime = 3;

    void Awake() 
    {
        instance = this;
    }
}
