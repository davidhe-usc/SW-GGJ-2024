using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{
    [SerializeField]
    Animator introAnimator;
    [SerializeField]
    string[] animationNames;
    [SerializeField]
    SimpleSpawner transitionSpawner;
    [SerializeField]
    string nextSceneName;
    DialogueRunner dialogueRunner;

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        StartCoroutine(Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Cutscene()
    {
        //set illustration motion
        introAnimator.Play(animationNames[0]);
        dialogueRunner.StartDialogue("Intro1");

        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        //change illustration motion if needed
        //do the same for the Intro2 node, and so on

        introAnimator.Play(animationNames[1]);
        dialogueRunner.StartDialogue("Intro2");

        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        introAnimator.Play(animationNames[2]);
        dialogueRunner.StartDialogue("Intro3");

        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        introAnimator.Play(animationNames[3]);
        dialogueRunner.StartDialogue("Intro4");

        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        introAnimator.Play(animationNames[4]);
        dialogueRunner.StartDialogue("Intro5");

        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        transitionSpawner.VagueSpawn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextSceneName);
    }
}
