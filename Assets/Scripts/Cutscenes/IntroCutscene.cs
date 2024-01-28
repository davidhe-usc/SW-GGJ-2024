using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class IntroCutscene : MonoBehaviour
{
    DialogueRunner dialogueRunner;

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Cutscene()
    {
        //set illustration motion

        dialogueRunner.StartDialogue("Intro1");

        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        //change illustration motion if needed
        //do the same for the Intro2 node, and so on
    }
}
