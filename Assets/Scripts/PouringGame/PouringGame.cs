using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PouringGame : MonoBehaviour
{
    public enum GameState {
        Active,
        Finished,
        Result
    };

    [SerializeField]
    public float timeLeft;
    [SerializeField]
    public int maxBalls;
    [SerializeField]
    public int dogVersionGoal;
    public bool isDogVersion;
    public GameState gameState;
    public int numBalls;
    public float maxTimeToSettle;
    public int ballsInTarget;
    public float calcTime;
    public float tempo;

    [SerializeField]
    private float tempoTimeMod;
    [SerializeField]
    private bool useTempoOverride;
    [SerializeField]
    private float tempoOverride;
    [SerializeField]
    private BoxCollider2D targetCollider;
    [SerializeField]
    private float maxTime;
    [SerializeField]
    private float dateVersionRatio;
    private float timeToSettle;

    public UnityEvent m_StartGame = new UnityEvent();
    public UnityEvent m_EndGame = new UnityEvent();
    public UnityEvent m_WinGame = new UnityEvent();
    public UnityEvent m_LoseGame = new UnityEvent();

    void Start()
    {
        gameState = GameState.Active;
        if (useTempoOverride)
        {
            tempo = tempoOverride;
        }
        else {
            tempo = TempoManager.tempo;
        }
        calcTime = maxTime - (tempo * tempoTimeMod);
        timeLeft = calcTime;
        m_StartGame.Invoke();
    }

    void Update()
    {
        ballsInTarget = GetBallsInTarget();
        if (gameState == GameState.Finished)
        {
            ballsInTarget = GetBallsInTarget();
            timeToSettle -= Time.deltaTime;
            if (timeToSettle <= 0f) {
                ShowResult();
            }
            return;
        }
        else if (gameState == GameState.Active)
        {
            if (!isDogVersion) {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0f || numBalls == maxBalls)
                {
                    EndGame();
                }
                return;
            }
            else if (GetSuccess()) {
                EndGame();
            }
        }
    }

    void ShowResult() {
        gameState = GameState.Result;
        if (GetSuccess() || isDogVersion) {
            m_WinGame.Invoke();
        }
        else {
            m_LoseGame.Invoke();
        }
    }

    void EndGame()
    {
        m_EndGame.Invoke();
        gameState = GameState.Finished;
        timeToSettle = maxTimeToSettle;
    }

    int GetBallsInTarget()
    {
        return Physics2D.OverlapAreaAll(targetCollider.bounds.min, targetCollider.bounds.max + new Vector3(0, 10, 0), LayerMask.GetMask("Balls")).Length;
    }

    bool GetSuccess()
    {
        if (isDogVersion) {
            return ballsInTarget >= dogVersionGoal;
        }
        else {
            return (numBalls >= 50 && ballsInTarget >= dateVersionRatio * numBalls);
        }
    }
}
