using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PouringUIController : MonoBehaviour
{
    [SerializeField]
    private PouringGame gameController;
    [SerializeField]
    Image timeBarFill;
    [SerializeField]
    Image timeBarSparks;
    private float currentTime;
    private float maxTime;

    void Start()
    {
        gameController.m_WinGame.AddListener(ShowWin);
        gameController.m_LoseGame.AddListener(ShowLose);
        gameController.m_StartGame.AddListener(StartTimer);
        StartTimer();
    }

    public void StartTimer()
    {
        maxTime = gameController.calcTime;
        currentTime = maxTime;
    }

    void Update()
    {
        if (gameController.isDogVersion) {
            return;
        }
        currentTime = Mathf.Max(0, currentTime - Time.deltaTime);
        timeBarFill.fillAmount = currentTime / maxTime;
        float sparkPosition = (1 - timeBarFill.fillAmount) * timeBarFill.rectTransform.sizeDelta.x;
        timeBarSparks.rectTransform.anchoredPosition = new Vector2(sparkPosition, 0);
        if (currentTime <= 0.15f)
        {
            timeBarSparks.GetComponent<Animator>().SetBool("Explode", true);
        }
    }

    void ShowWin()
    {
        print("win");
        if (!TempoManager.instance)
        {
            return;
        }

        if (gameController.isDogVersion)
        {
            TempoManager.instance.DogMinigameEnd();
        }
        else {
            TempoManager.instance.MinigameEnd(true);
        }
    }
    
    void ShowLose()
    {
        print("lose");
        if (!TempoManager.instance)
        {
            return;
        }

        if (gameController.isDogVersion) // shouldnt be necessary but just in case
        {
            TempoManager.instance.DogMinigameEnd();
        }
        else {
            TempoManager.instance.MinigameEnd(false);
        }
    }
}
