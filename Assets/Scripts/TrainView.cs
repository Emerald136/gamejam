using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainView : MonoBehaviour
{
    public static TrainView instance;

    [SerializeField] public Transform train;
    [SerializeField] public Transform player;
    [SerializeField] public TextMeshProUGUI countdownText;
    [SerializeField] public GameObject TextView;

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
