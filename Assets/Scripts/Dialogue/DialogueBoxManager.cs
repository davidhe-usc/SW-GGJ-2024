using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueBoxManager : MonoBehaviour
{
    static DialogueRunner dialogueRunner;

    static LineView Normal;
    static LineView Genuine;
    static LineView Thought;
    static LineView Date;

    // Start is called before the first frame update
    void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        Normal = GameObject.Find("MainDialogue").GetComponent<LineView>();
        Genuine = GameObject.Find("GenuineDialogue").GetComponent<LineView>();
        Thought = GameObject.Find("ThoughtDialogue").GetComponent<LineView>();
        Date = GameObject.Find("DateDialogue").GetComponent<LineView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [YarnCommand("ChangeView")]
    public static void ChangeView(string view)
    {
        switch (view)
        {
            case ("normal"):
                DialogueViewBase[] views1 = { Normal };
                dialogueRunner.SetDialogueViews(views1);
                break;
            case ("genuine"):
                DialogueViewBase[] views2 = { Genuine };
                dialogueRunner.SetDialogueViews(views2);
                break;
            case ("thought"):
                DialogueViewBase[] views3 = { Thought };
                dialogueRunner.SetDialogueViews(views3);
                break;
            case ("date"):
                DialogueViewBase[] views4 = { Date };
                dialogueRunner.SetDialogueViews(views4);
                break;
        }
    }
}
