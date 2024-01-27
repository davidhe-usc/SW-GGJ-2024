using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Answer : MonoBehaviour
{
    TextMeshProUGUI answerText;

    int answerType = 0; //1 is genuine, 0 is neutral, -1 is wrong.

    float angle = 0;

    // Start is called before the first frame update
    void Awake()
    {
        answerText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        answerText.text = text;
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
    }

    public int GetAnswerType()
    {
        return answerType;
    }
}
