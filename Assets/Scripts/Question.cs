using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;

public class Question : MonoBehaviour
{
    TempoManager tm;

    TextMeshProUGUI question;

    Answer[] answers; //Should be 4

    float radius = 80f; //Distance from center of question to the target

    private float tempo = 90; //Degrees per second

    bool answered = false; //Whether the player has answered

    [SerializeField] GameObject AnswerPrefab;

    private void Awake()
    {
        tm = TempoManager.instance;
        tempo = tm.GetTempo();

        question = GetComponentInChildren<TextMeshProUGUI>();

        answers = new Answer[4];

        float angleModifier = 0;
        for (int x = 0; x < answers.Length; x++)
        {
            answers[x] = Instantiate(AnswerPrefab, this.transform).GetComponent<Answer>();
            answers[x].transform.position = PointOnCircle(angleModifier, radius);
            answers[x].SetAngle(angleModifier);
            angleModifier += Mathf.PI/2;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!answered)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //When the player presses space, or whatever we decide in the end, select the answer closest to the top.
            {
                foreach (Answer a in answers)
                {
                    if (a.transform.localPosition.y > 70) //Testing value
                    {
                        tm.ReceiveAnswer(a.GetAnswerType());
                        answered = true;
                        //Do some kind of effect on the selected answer so the player knows what they've done. Also have the questions move more so the selected one ends up in the middle of the selection circle.
                    }
                }
            }

            foreach (Answer a in answers)
            {
                float angle = a.GetAngle();
                angle -= tempo * Time.deltaTime;
                a.SetAngle(angle);
                a.transform.localPosition = PointOnCircle(angle, radius);
            }
        }
    }

    Vector3 PointOnCircle(float angle, float r)
    {
        return new Vector3(radius * Mathf.Cos(angle), r * Mathf.Sin(angle), 0);
    }

    public void SetAnswerText(string[] texts, int[] types) //Set the answer text and whether they're a genuine, neutral, or bad response
    {
        for(int x = 0; x<answers.Length; x++)
        {
            print(x);
            print(texts[x]);
            answers[x].SetText(texts[x]);
            answers[x].SetAnswerType(types[x]);
        }
    }

    public void SetQuestionText(string q)
    {
        question.text = q;
    }
}
