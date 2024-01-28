using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class TempoManager : MonoBehaviour
{
    public static TempoManager instance;

    private DialogueRunner dialogueRunner;

    public static float tempo = 0f;
    private static int genuineAnswers = 0;
    private static int availableGenuine = 0;
    private int endThreshold = 4; //number of genuine responses for the date to end.
    private int tempoLimit = 40; //Max tempo before the date ends.

    private int dogWins = 0;
    private int dateWins = 0;

    public GameObject questionPrefab;

    public Canvas canvas;

    int minigameCount = 0;
    int nextMinigame; //set to 1 for hose, 2 for pies.

    //Questions
    Dictionary<string, Dictionary<string, int>> questionList;

    string questionName;

    private Question activeQuestion;

    List<string> usedQuestions;

    int questionCount = 0; //current number of question
    int questionCap = 1; //how many questions in the current set

    // Start is called before the first frame update
    void Awake()
    {
        TempoManager[] tms = FindObjectsOfType<TempoManager>();

        if (tms.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    private void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();

        //Setting up questions dictionary
        Dictionary<string, int> q1 = new Dictionary<string, int>{
            {"Would you love me if I was a worm?", 0},
            {"Of course I would!", 0},
            {"I'd love to be a worm too.", 0},
            {"I'd eat you (hungrily).", -12},
            {"I'd eat you (lovingly).", -12},
            {"I would respect you.", 8}};
        Dictionary<string, int> q2 = new Dictionary<string, int>{
            {"Can you smell my feet?", 0 },
            {"Nope!", 0},
            {"I can't smell.", -4},
            {"I'd love to!", -12},
            {"Yes.", -12},
            {"I don't think I can.", 8}};
        Dictionary<string, int> q3 = new Dictionary<string, int>{
            {"Would you like a bite of my dish?", 0 },
            {"Yes, please!", 0},
            {"No, thank you.", 0},
            {"<i>[Take Multiple Bites]</i>", -12},
            {"<i>[Bite The Dish]</i>", -12},
            {"Would you like a bite of mine?", 8}};
        Dictionary<string, int> q4 = new Dictionary<string, int>{
            {"Do you think cereal is a soup?", 0 },
            {"Sure!", 0},
            {"Does it matter?", -4},
            {"Your toilet could be soup.", -8},
            {"Human soup.", -8},
            {"Let me think about that?", 8}};
        Dictionary<string, int> q5 = new Dictionary<string, int>{
            {"How was your day so far?", 0 },
            {"Good!", 0},
            {"Not good.", -4},
            {"I just woke up.", -4},
            {"Why do you want to know?", -8},
            {"How was yours?", 8}};
        Dictionary<string, int> q6 = new Dictionary<string, int>{
            {"How's your relationship with your family?", 0 },
            {"It's okay.", 0},
            {"I don't talk about my family.", -8},
            {"I was just born.", -4},
            {"Bad.", -12},
            {"It's like any other relationship.", 8}};
        Dictionary<string, int> q7 = new Dictionary<string, int>{
            {"Do you like animals?", 0 },
            {"Yes.", 0},
            {"No.", 0},
            {"I like... to eat them.", -4},
            {"<i>[Begin Making Balloon Animals]</i>", -8},
            {"I have a pet dog.", 8}};
        Dictionary<string, int> q8 = new Dictionary<string, int>{
            {"What do you do for hobbies?", 0 },
            {"I juggle.", 0},
            {"I read.", 0},
            {"I don't have hobbies.", -4},
            {"I invest in crypto.", -8},
            {"I like to try new things!", 8}};
        Dictionary<string, int> q9 = new Dictionary<string, int>{
            {"What's a fun fact about you?", 0 },
            {"I'm a clown.", 0},
            {"I can play my nose like an instrument.", -4},
            {"I'm pretty boring.", -4},
            {"I eat my own hair.", -12},
            {"I love to travel!", 8}};
        Dictionary<string, int> q10 = new Dictionary<string, int>{
            {"Do you like your job?", 0 },
            {"It's a job.", 0},
            {"Yes.", 0},
            {"No.", 0},
            {"I hate it.", -8},
            {"I do!", 8}};
        Dictionary<string, int> q11 = new Dictionary<string, int>{
            {"What are the three digits on the back of your credit card?", 0 },
            {"Uh...", 0},
            {"Oh, they're...", -4},
            {"My credit is really bad.", -8},
            {"No, thanks!", -8},
            {"You're funny!", 8}};

        questionList = new Dictionary<string, Dictionary<string, int>>() {
            {"Question1", q1},
            {"Question2", q2},
            {"Question3", q3},
            {"Question4", q4},
            {"Question5", q5},
            {"Question6", q6},
            {"Question7", q7},
            {"Question8", q8},
            {"Question9", q9},
            {"Question10", q10},
            {"Question11", q11},
        };

        usedQuestions = new List<string>();

        nextMinigame = Random.Range(1, 3);

        questionCap = Random.Range(1, 4);

        Next(); //Temporary instant start
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveAnswer(int type, int number)
    { 
        if(type >= 1) //Genuine answer
        {
            tempo -= type;
            if (tempo < 0)
                tempo = 0;
            genuineAnswers += 1;
            availableGenuine -= 1;
            usedQuestions.Add(questionName);
            if(genuineAnswers >= endThreshold)
            {
                //end the game
            }
        }
        else //Otherwise, increase tempo by how wrong the answer was
        {
            tempo += type * -1;
        }
        AudioTempoHandling.instance.ChangeAudioTempo(type * -1);
        StartCoroutine(AnswerDelay(type, number));
    }

    IEnumerator AnswerDelay(int type, int number)
    {
        yield return new WaitForSeconds(2f);

        GameObject.Destroy(activeQuestion.gameObject);

        if (number < 6) //not a honk
            dialogueRunner.StartDialogue(questionName + "Response" + number);
        else
            Next();
    }

    [YarnCommand("question")]
    public void CreateQuestion(string question)
    {
        activeQuestion = Instantiate(questionPrefab, canvas.transform).GetComponent<Question>();
        activeQuestion.transform.localPosition = Vector3.zero;

        string[] texts = new string[4];
        int[] types = new int[4];

        int i = 0;
        foreach (KeyValuePair<string, int> k in questionList[question])
        {
            Debug.Log(i + ": " + k.Key);
            if(i==0)
                activeQuestion.SetQuestionText(k.Key);
            else if(i<5)
            {
                texts[i-1] = k.Key;
                types[i-1] = k.Value;
            }
            else if(availableGenuine > 0) //The 6th is the genuine answer. If one is supposed to appear, randomly replace one of the other options with it.
            {
                int r = Random.Range(0, 5);
                texts[r] = k.Key;
                types[r] = k.Value;
            }
            i++;
        }
        activeQuestion.SetAnswerText(texts, types);
    }

    [YarnCommand("next")]
    public void Next()
    {
        if (questionCount < questionCap)
        {
            questionCount++;

            do
            {
                questionName = "Question" + Random.Range(1, 12);
            } while (usedQuestions.Contains(questionName));

            StartCoroutine(NextDialogue(questionName));
        }
        else
        {
            if(tempo>tempoLimit)
            {
                StartCoroutine(NextDialogue("MissDog"));
            }    
            else if (nextMinigame == 1)
            {
                SceneManager.LoadScene("PouringDate");
            }
            else
            {
                SceneManager.LoadScene("PieToss");
            }
        }
    }

    IEnumerator NextDialogue(string node)
    {
        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        yield return new WaitForSeconds(2f);

        dialogueRunner.StartDialogue(node);
    }

    public void MinigameEnd(bool win)
    {
        SceneManager.LoadScene("Date");

        //transitions and pauses

        minigameCount++;
        if (minigameCount < 2)
        {
            if (nextMinigame == 1)
                nextMinigame = 2;
            else
                nextMinigame = 1;
        }
        else
            nextMinigame = Random.Range(1, 3);

        questionCap = Random.Range(1, 4);

        questionCount = 0;

        if (win)
        {
            availableGenuine += 1;
            tempo += 4;
            if (dateWins < 4)
                dateWins++;

            NextDialogue("DateWin" + dateWins);
        }
        else
        {
            tempo += 12;
            Next();
        }
    }

    [YarnCommand("loadDogGame")]
    public void LoadDogGame()
    {
        if (nextMinigame == 1)
        {
             SceneManager.LoadScene("PouringDog");
        }
        else
        {
             SceneManager.LoadScene("DogToss");
        }
    }

    public void DogMinigameEnd()
    {
        tempo = tempo / 2;

        SceneManager.LoadScene("Date");

        //transitions and pauses

        minigameCount++;
        if (minigameCount < 2)
        {
            if (nextMinigame == 1)
                nextMinigame = 2;
            else
                nextMinigame = 1;
        }
        else
            nextMinigame = Random.Range(1, 3);

        questionCap = Random.Range(1, 4);

        questionCount = 0;

        if (dogWins < 4)
            dogWins++;

        NextDialogue("DogWin" + dogWins);
    }
}
