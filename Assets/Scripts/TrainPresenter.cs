using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class TrainPresenter : MonoBehaviour
{
    public static TrainPresenter instance;

    public bool isMovingTrain;

    void Awake() 
    {
        instance = this;
    }

    public void PlayerOnTheTrain() 
    {
        TrainView.instance.TextView.SetActive(true);
        StartCoroutine(ReportBeforeMovement());
    }

    IEnumerator ReportBeforeMovement() 
    {
        for (int i = TrainModel.instance.countdownTime + 1; i > -1; i--)
        {
            if (TrainView.instance.countdownText != null && i == 4)
            {
                TrainView.instance.countdownText.text = "Не выходите из поезда";
            }
            else if (TrainView.instance.countdownText != null) 
            {
                TrainView.instance.countdownText.text = i.ToString();
            }

            Debug.Log("Отсчет: " + i);
            if (i == 0) 
            {
                MoveTrain();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void MoveTrain() 
    {
        Debug.Log("Move");
        isMovingTrain = true;
        TrainView.instance.TextView.SetActive(false);
        TrainModel.instance.countdownTime = 3;
        StartCoroutine(MoveTrainAnim());

    }

    private IEnumerator MoveTrainAnim() 
{
    float travelDuration = 1.5f;
    float elapsedTime = 0f;

    // Отключаем физику игрока
    Rigidbody2D playerRb = TrainView.instance.player.GetComponent<Rigidbody2D>();
    if (playerRb != null)
    {
        playerRb.isKinematic = true;
    }

    Vector3 startPositionTrain = TrainView.instance.train.position;
    Vector3 endPositionTrain = startPositionTrain + new Vector3(100f, 0f, 0f);

    Vector3 playerOffset = TrainView.instance.player.position - startPositionTrain;

    while (elapsedTime < travelDuration) 
    {
        TrainView.instance.train.position = Vector3.Lerp(startPositionTrain, endPositionTrain, elapsedTime / travelDuration);
        TrainView.instance.player.position = TrainView.instance.train.position + playerOffset;
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    TrainView.instance.train.position = endPositionTrain;
    TrainView.instance.player.position = endPositionTrain + playerOffset;

    // Включаем физику обратно
    if (playerRb != null)
    {
        playerRb.isKinematic = false;
    }

    isMovingTrain = false;
}

    public void StopCurrutines() 
    {
        StopAllCoroutines();
        TrainView.instance.TextView.SetActive(false);
        TrainModel.instance.countdownTime = 3;
    }
}
