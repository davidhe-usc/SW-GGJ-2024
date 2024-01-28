using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MinigameTimer : MonoBehaviour
{
    [SerializeField] Image timeBar;
    [SerializeField] Image timeBarFill;
    [SerializeField] Image timeBarSparks;

    [SerializeField]
    float maxTime;
    [SerializeField]
    float transitionGraceTime;
    [SerializeField]
    float tempoMultiplier;

    [SerializeField]
    UnityEvent timeUpEvent;

    float calculatedStartTime;
    float currentTime;
    bool canLose = true;

    // Start is called before the first frame update
    void Start()
    {
        calculatedStartTime = maxTime - (TempoManager.tempo * -1 * tempoMultiplier) + transitionGraceTime;
        currentTime = calculatedStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        timeBarFill.fillAmount = currentTime / calculatedStartTime;
        float sparkPosition = (1 - timeBarFill.fillAmount) * timeBarFill.rectTransform.sizeDelta.x;
        timeBarSparks.rectTransform.anchoredPosition = new Vector2(sparkPosition, 0);
        if (currentTime <= 0.15f)
        {
            timeBarSparks.GetComponent<Animator>().SetBool("Explode", true);
        }

        if (currentTime <= 0)
        {
            if (canLose == true)
            {
                canLose = false;
                timeUpEvent.Invoke();
            }
        }
    }

    public void IgnoreTimer()
    {
        canLose = false;
    }
}
