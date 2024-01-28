using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI answerText;
    [SerializeField] TextMeshProUGUI answerShadow;

    int answerType = 0; //1 is genuine, 0 is neutral, -1 is wrong.

    float angle = 0;

    int end = 0; //1 for chosen, 0 for still choosing, -1 for dropped

    float xVel = 0; //For dropping animation
    float yVel = 0;

    float targetHeight = -221f;

    [SerializeField] Image textBackground;
    [SerializeField] Sprite genuineBall;
    [SerializeField] Sprite genuineBackground;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (end == 1)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, targetHeight, 0), 0.01f);
        }
        else if (end == -1)
        {
            Vector3 pos = transform.localPosition;
            pos += new Vector3(xVel*Time.deltaTime, yVel*Time.deltaTime, 0);
            transform.localPosition = pos;
            yVel -= 600*Time.deltaTime;
        }
    }

    public void SetText(string text)
    {
        answerText.text = text;
        answerShadow.text = text;
    }

    public void SetAngle(float a)
    {
        angle = a;
    }

    public float GetAngle()
    {
        return angle;
    }
    public void SetAnswerType(int a)
    {
        answerType = a;
        if (a > 0)
        {
            GetComponent<Image>().sprite = genuineBall;
            textBackground.sprite = genuineBackground;
        }
    }

    public int GetAnswerType()
    {
        return answerType;
    }

    public void End(int chosen)
    {
        end = chosen;
        if(end < 0)
        {
            yVel = Random.Range(200, 600);
            xVel = Random.Range(-400, 400);
        }
    }
}
