using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using TMPro;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    TempoManager tm;

    TextMeshProUGUI question;

    Answer[] answers; //Should be 4

    List<Answer> answerList;

    float radius = 134f; //Distance from center of question to the target

    private float tempo = 0; //Radians per second. An orbit completes in .75*(2pi/tempo) seconds

    bool answered = false; //Whether the player has answered

    bool answersPopulated = false; //Whether all answers have been created.

    float questionTimer = 6f;

    Vector3 answerOrigin;

    float targetHeight = -221f;
    float targetHeightThreshold;



    [SerializeField] GameObject AnswerPrefab;
    [SerializeField] Slider TimeBar;

    private void Awake()
    {
        tempo = TempoManager.tempo;

        question = GetComponentInChildren<TextMeshProUGUI>();

        answers = new Answer[4];

        answerOrigin = new Vector3(-7f, -355f, 0);

        for (int x = 0; x < answers.Length; x++)
        {
            answers[x] = Instantiate(AnswerPrefab, this.transform).GetComponent<Answer>();
            answers[x].transform.localPosition = answerOrigin + PointOnCircle(Mathf.PI, radius);
            answers[x].SetAngle(Mathf.PI);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targetHeightThreshold = targetHeight - 20;
        answerList = new List<Answer>();
        StartCoroutine(MoveAnswers());
    }

    // Update is called once per frame
    void Update()
    {
        if (!answered)
        {
            if (answersPopulated)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) //When the player presses space, or whatever we decide in the end, select the answer closest to the top.
                {
                    foreach (Answer a in answerList)
                    {
                        if (a.transform.localPosition.y > targetHeightThreshold) //Testing value.
                        {
                            TempoManager.instance.ReceiveAnswer(a.GetAnswerType());
                            answered = true;
                            a.End(1);
                        }
                        else
                        {
                            a.End(-1);
                        }
                    }
                }

                questionTimer -= Time.deltaTime;
                if(questionTimer<=0)
                {
                    //Honk.
                    questionTimer = 0;
                    TempoManager.instance.ReceiveAnswer(-2); //Very bad answer
                    answered = true;
                    foreach (Answer a in answerList)
                        a.End(-1);
                }
                TimeBar.value = questionTimer;
            }

            foreach (Answer a in answerList)
            {
                float angle = a.GetAngle();
                if (a.transform.localPosition.y > answerOrigin.y)
                    angle -= 2* (2f + (tempo/8f)) * Time.deltaTime;
                else
                    angle -= (2f + (tempo/8f)) * Time.deltaTime;
                a.SetAngle(angle);
                a.transform.localPosition = answerOrigin + PointOnCircle(angle, radius);
            }
        }
    }

    IEnumerator MoveAnswers()
    {
        for (int x = 0; x < answers.Length; x++)
        {
            Debug.Log("Answer: " + x);
            answerList.Add(answers[x]); //Add answers to the list so they can start moving.
            yield return new WaitForSeconds((0.75f*((2f*Mathf.PI)/(2f+(tempo/8f)))) / answers.Length);
        }
        answersPopulated = true;
        TimeBar.gameObject.SetActive(true);
    }

    Vector3 PointOnCircle(float angle, float r)
    {
        return new Vector3(r * Mathf.Cos(angle), r * Mathf.Sin(angle), 0);
    }

    public void SetAnswerText(string[] texts, int[] types) //Set the answer text and whether they're a genuine, neutral, or bad response
    {
        for(int x = 0; x<answers.Length; x++)
        {
            answers[x].SetText(texts[x]);
            answers[x].SetAnswerType(types[x]);
        }
    }

    public void SetQuestionText(string q)
    {
        question.text = q;
    }
}
