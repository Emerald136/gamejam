using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainViewOne : MonoBehaviour
{
    public static TrainViewOne instance;

    [SerializeField] public Transform train;
    [SerializeField] public TextMeshProUGUI countdownText;
    [SerializeField] public GameObject TextView;
    [SerializeField] public Image blackBackground;

    public bool isMoved;

    void Awake() 
    {
        instance = this;
    }

}
