using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackAndForthSlider : MonoBehaviour
{
    [SerializeField]
    GameObject throwingObject;
    [SerializeField]
    float unmodifiedSpeed;
    [SerializeField]
    float slideAreaWidth;
    [SerializeField]
    float offset = 0;
    [SerializeField]
    float initialPosition;
    bool sliding = true;
    [SerializeField]
    FieldGoalChecker fieldGoalChecker;
    [SerializeField]
    UnityEvent succeedEvent;
    [SerializeField]
    UnityEvent failEvent;
    [SerializeField]
    UnityEvent resumeEvent;
    [SerializeField]
    float tempoMidpoint;
    [SerializeField]
    float earlyTempoFactor;
    [SerializeField]
    float lateTempoFactor;
    float currentSpeed;

    float timeCounter = 0;

    [SerializeField] AudioClipCueSO sfxPieThrow, sfxPieLand, sfxPieMiss;

    // Start is called before the first frame update
    void Start()
    {
        if (fieldGoalChecker == null)
        {
            fieldGoalChecker = GameObject.FindObjectOfType<FieldGoalChecker>();
        }
        currentSpeed = unmodifiedSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && sliding == true)
        {
            //SOUND - ThrowSound.Play();
            SFXOneShots.instance.PlayOneShot(SFXOneShots.instance.sfxPieThrow);

            sliding = false;
            if (fieldGoalChecker.IsWithinSuccessBounds() == true)
            {
                SFXOneShots.instance.PlayOneShot(SFXOneShots.instance.sfxPieLand);
                succeedEvent.Invoke();
            } else
            {
                SFXOneShots.instance.PlayOneShot(SFXOneShots.instance.sfxPieMiss);
                failEvent.Invoke();
            }
        }
        if (sliding == true)
        {
            /* COS version */
            float tempoModifier = 0;
            float tempoRemaining = TempoManager.tempo * -1;
            if (tempoRemaining > tempoMidpoint) //The tempo you get under halfway to max counts less than the tempo you get past the halfway point to max (since the jump from 2 to 3 would be big but the jump from 12 to 13 would be small).
            {
                tempoModifier += lateTempoFactor * (tempoRemaining - tempoMidpoint);
                tempoRemaining = tempoMidpoint;
            }
            tempoModifier += tempoRemaining * earlyTempoFactor;
            currentSpeed = unmodifiedSpeed + tempoModifier;

            timeCounter += Time.deltaTime * currentSpeed;
            
            throwingObject.transform.position = new Vector2(Mathf.Cos(timeCounter) * slideAreaWidth + offset, throwingObject.transform.position.y);
            if (timeCounter >= Mathf.Deg2Rad * 360)
            {
                timeCounter -= Mathf.Deg2Rad * 360;
            }
        }
    }

    public void ResumeSlide()
    {
        resumeEvent.Invoke();
        timeCounter = initialPosition;
        sliding = true;
    }
}
