using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieWinCheck : MonoBehaviour
{
    [SerializeField]
    int successesNeeded;
    int currentSuccesses = 0;

    [SerializeField]
    UnityEvent loseEvent;
    [SerializeField]
    UnityEvent winEvent;
    [SerializeField]
    PieScoreCounter pieScoreCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetSuccessesNeeded()
    {
        return successesNeeded;
    }
    public int GetCurrentSuccesses()
    {
        return currentSuccesses;
    }
    public void GainSuccess()
    {
        currentSuccesses++;
        if (pieScoreCounter != null)
        {
            pieScoreCounter.UpdateTextWithFlourish(); 
        }
        if (currentSuccesses >= successesNeeded)
        {
            winEvent.Invoke();
        }
    }

    public void Fail()
    {
        loseEvent.Invoke();
    }

    public void CheckIfFailed()
    {
        StartCoroutine(WaitForFinalScoreTally());
    }
    IEnumerator WaitForFinalScoreTally()
    {
        yield return new WaitForSeconds(0.02f);
        if (currentSuccesses < successesNeeded) {
            Fail();
        }
    }
}
