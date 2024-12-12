using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainViewTwo : MonoBehaviour
{
    public static TrainViewTwo instance;

    [SerializeField] public Transform train;
    [SerializeField] public TextMeshProUGUI countdownText;
    [SerializeField] public GameObject TextView;
    [SerializeField] public Image blackBackground;

    public bool isMoved;

    void Awake() 
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !TrainPresenter.instance.isMovingTrain)
        {
            TrainPresenter.instance.PlayerOnTheTrain();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !TrainPresenter.instance.isMovingTrain)
        {
            TrainPresenter.instance.StopCurrutines();
        }
    }
}
