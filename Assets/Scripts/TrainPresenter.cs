using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainPresenter : MonoBehaviour
{
    public static TrainPresenter instance;

    public bool isMovingTrain;
    private Transform player;
    void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        player = GameObject.FindWithTag("Player").transform;
        StartScene(400);
    }

    public void StartScene(float startOffset)
    {
        TrainViewOne.instance.blackBackground.color = new Color(0f, 0f, 0f, 1f);
        TrainModel.instance.moveOneTrain = true;
        Vector3 finalPosition = TrainViewOne.instance.train.position;

        Vector3 startPosition = new Vector3(finalPosition.x - startOffset, finalPosition.y, finalPosition.z);
        TrainViewOne.instance.train.position = startPosition;
        StartCoroutine(BlackoutRestartScene(4));
        player.position = startPosition;

        StartCoroutine(MoveTrainToTarget(finalPosition));
    }

    private IEnumerator MoveTrainToTarget(Vector3 finalPosition)
    {
        float travelDuration = 3f;
        float elapsedTime = 0f;

        Vector3 startPosition = TrainViewOne.instance.train.position;

        Vector3 playerOffset = player.position - startPosition;

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.isKinematic = true;
        }

        while (elapsedTime < travelDuration)
        {
            elapsedTime += Time.deltaTime;

            TrainViewOne.instance.train.position = Vector3.Lerp(startPosition, finalPosition, elapsedTime / travelDuration);

            player.position = TrainViewOne.instance.train.position + playerOffset;

            yield return null;
        }

        TrainViewOne.instance.train.position = finalPosition;
        player.position = finalPosition + playerOffset;

        if (playerRb != null)
        {
            playerRb.isKinematic = false;
        }
        
        TrainModel.instance.moveOneTrain = false;
    }

    private IEnumerator BlackoutRestartScene(float chislo)
    {
        yield return new WaitForSeconds(0.8f);
        float duration = chislo;
        float time = 0f;

        float firstZatemnenie = 1f;
        float lastZatemnenie = 0f;

        Color initialColor = TrainViewOne.instance.blackBackground.color;

        initialColor.a = firstZatemnenie;
        TrainViewOne.instance.blackBackground.color = initialColor;
        while (time < duration)
        {
            time += Time.deltaTime;

            initialColor.a = Mathf.Lerp(firstZatemnenie, lastZatemnenie, time / duration);

            TrainViewOne.instance.blackBackground.color = initialColor;

            yield return null;
        }

        initialColor.a = lastZatemnenie;
        TrainViewOne.instance.blackBackground.color = initialColor;
    }

    public void PlayerOnTheTrain() 
    {
        if (!TrainModel.instance.moveOneTrain) 
        {
            TrainViewTwo.instance.TextView.SetActive(true);
            StartCoroutine(ReportBeforeMovement());
        }

    }

    IEnumerator ReportBeforeMovement() 
    {
        for (int i = TrainModel.instance.countdownTime + 1; i > -1; i--)
        {
            if (TrainViewTwo.instance.countdownText != null && i == 4)
            {
                TrainViewTwo.instance.countdownText.text = "Не выходите из поезда";
            }
            else if (TrainViewTwo.instance.countdownText != null) 
            {
                TrainViewTwo.instance.countdownText.text = i.ToString();
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
        isMovingTrain = true;
        TrainViewTwo.instance.TextView.SetActive(false);
        TrainModel.instance.countdownTime = 3;
        StartCoroutine(MoveTrainAnim());

    }

    private IEnumerator MoveTrainAnim() 
{
    TrainViewTwo.instance.isMoved = true;
    float travelDuration = 8f;
    float elapsedTime = 0f;

    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
    if (playerRb != null)
    {
        playerRb.isKinematic = true;
    }

    Vector3 startPositionTrain = TrainViewTwo.instance.train.position;
    Vector3 endPositionTrain = startPositionTrain + new Vector3(300f, 0f, 0f);

    Vector3 playerOffset = player.position - startPositionTrain;

    StartCoroutine(Blackout());

    while (elapsedTime < travelDuration) 
    {
        TrainViewTwo.instance.train.position = Vector3.Lerp(startPositionTrain, endPositionTrain, elapsedTime / travelDuration);

        player.position = TrainViewTwo.instance.train.position + playerOffset;

        elapsedTime += Time.deltaTime;

        yield return null;
    }

    TrainViewTwo.instance.train.position = endPositionTrain;

    player.position = endPositionTrain + playerOffset;

    if (playerRb != null)
    {
        playerRb.isKinematic = false;
    }

    isMovingTrain = false;
    TrainModel.instance.moveOneTrain = true;
}

    private IEnumerator Blackout()
    {
        float duration = 3f;
        float time = 0f;

        float firstZatemnenie = 0f;
        float lastZatemnenie = 1f;

        Color initialColor = TrainViewTwo.instance.blackBackground.color;

        initialColor.a = firstZatemnenie;
        TrainViewTwo.instance.blackBackground.color = initialColor;

        while (time < duration)
        {
            time += Time.deltaTime;

            initialColor.a = Mathf.Lerp(firstZatemnenie, lastZatemnenie, time / duration);

            TrainViewTwo.instance.blackBackground.color = initialColor;

            yield return null;
        }

        initialColor.a = lastZatemnenie;
        TrainViewTwo.instance.blackBackground.color = initialColor;
        SwipeGunsModel.instance.SaveCurrentGun();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StopCurrutines() 
    {
        StopAllCoroutines();
        TrainViewTwo.instance.TextView.SetActive(false);
        TrainModel.instance.countdownTime = 3;
    }

    
}
