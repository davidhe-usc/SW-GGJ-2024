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

    float timeCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (fieldGoalChecker == null)
        {
            fieldGoalChecker = GameObject.FindObjectOfType<FieldGoalChecker>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && sliding == true)
        {
            //SOUND - ThrowSound.Play();
            sliding = false;
            if (fieldGoalChecker.IsWithinSuccessBounds() == true)
            {
                succeedEvent.Invoke();
            } else
            {
                failEvent.Invoke();
            }
        }
        if (sliding == true)
        {
            /* COS version */
            timeCounter += Time.deltaTime * unmodifiedSpeed;
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
