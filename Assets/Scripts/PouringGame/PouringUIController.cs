using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PouringUIController : MonoBehaviour
{
    [SerializeField]
    private PouringGame gameController;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI ballCountText;
    [SerializeField]
    private TextMeshProUGUI resultText;

    void Start()
    {
        gameController.m_WinGame.AddListener(ShowWin);
        gameController.m_LoseGame.AddListener(ShowLose);
    }

    void Update()
    {
        timerText.text = string.Format("time left: {0}", Mathf.Ceil(gameController.timeLeft));
        ballCountText.text = string.Format("balls left: {0}", gameController.maxBalls - gameController.numBalls);
    }

    void ShowWin()
    {
        resultText.text = "yay yipee";
    }
    
    void ShowLose()
    {
        resultText.text = "oh no";
    }
}
