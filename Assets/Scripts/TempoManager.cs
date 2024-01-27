using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TempoManager : MonoBehaviour
{
    public static TempoManager instance;

    private DialogueRunner dialogueRunner;

    private Question activeQuestion;

    private float tempo = 2f;
    private int genuineAnswers = 0;

    public GameObject questionPrefab;

    public Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetTempo()
    {
        return tempo;
    }

    public void ReceiveAnswer(int type)
    { 
        if(type >= 1) //Genuine answer
        {
            tempo -= 1;
            genuineAnswers += 1;
        }
        else //Otherwise, increase tempo by how wrong the answer was
        {
            tempo += type * -1;
        }
        StartCoroutine(AnswerDelay(type));
    }

    IEnumerator AnswerDelay(int type)
    {
        yield return new WaitForSeconds(2f);

        GameObject.Destroy(activeQuestion.gameObject);

        if (type >= 1)
        {
            dialogueRunner.StartDialogue("GenuineResponse");
        }
        else if (type >= -1)
        {
            dialogueRunner.StartDialogue("MehResponse");
        }
        else
        {
            dialogueRunner.StartDialogue("BadResponse");
        }
    }

    [YarnCommand("question")]
    public void CreateQuestion(string question)
    {
        //To do: set up a system to organize questions and decide which one is created. For now they're placeholders.
        activeQuestion = Instantiate(questionPrefab, canvas.transform).GetComponent<Question>();
        activeQuestion.transform.localPosition = new Vector3(0, -125f, 0);
        string[] texts = { "It's not my thing.", "It's a waste of time", "What's that?", "Can we placehold hands?"};
        int[] types = { 1, -2, -1, -2 };
        activeQuestion.SetAnswerText(texts, types);
        activeQuestion.SetQuestionText("How do you feel about placeholder text?");
    }
}
