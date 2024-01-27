using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieLives : MonoBehaviour
{
    [SerializeField]
    int startingTries;
    [SerializeField]
    GameObject lifeCounter;
    [SerializeField]
    GameObject lifeIconPrefab;
    [SerializeField]
    UnityEvent endEvent;
    [SerializeField]
    PieWinCheck pieWinCheck;
    int currentTry = 99;
    // Start is called before the first frame update
    void Start()
    {
        currentTry = startingTries;
        for (int i = 0; i < startingTries; i++)
        {
            Instantiate(lifeIconPrefab, lifeCounter.transform);
        }
    }

    public void ShowMiss()
    {
        lifeCounter.transform.GetChild(startingTries - currentTry).GetComponent<PieIconSuccessState>().ShowFail();
        currentTry--;
        //Object.Destroy(lifeCounter.transform.GetChild(0).gameObject);
        if (currentTry <= 0)
        {
            pieWinCheck.CheckIfFailed();
        }
    }

    public void ShowHit()
    {
        lifeCounter.transform.GetChild(startingTries - currentTry).GetComponent<PieIconSuccessState>().ShowSuccess();
        currentTry--;
        //Object.Destroy(lifeCounter.transform.GetChild(0).gameObject);
        if (currentTry <= 0)
        {
            pieWinCheck.CheckIfFailed();
        }
    }
}
