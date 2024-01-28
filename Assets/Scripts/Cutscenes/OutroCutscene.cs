using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class OutroCutscene : MonoBehaviour
{
    DialogueRunner dialogueRunner;

    [SerializeField] Image fade;
    [SerializeField] GameObject back1;
    [SerializeField] GameObject subject1;
    [SerializeField] GameObject back2;
    [SerializeField] GameObject subject2;
    [SerializeField] GameObject back3;
    [SerializeField] GameObject fore3;
    [SerializeField] GameObject subject3;

    [SerializeField] Vector3[] positions;

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

        StartCoroutine(FadeImage(-1, fade));

        yield return new WaitForSeconds(2f);

        dialogueRunner.StartDialogue("Outro1");

        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        //this is where we animate the background

        yield return new WaitForSeconds(1f);

        dialogueRunner.StartDialogue("Outro2");

        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        StartCoroutine(FadeImage(1, fade));

        yield return new WaitForSeconds(1f);

        back1.gameObject.SetActive(false);
        subject1.gameObject.SetActive(false);
        back2.gameObject.SetActive(true);
        subject2.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(FadeImage(-1, fade));

        yield return new WaitForSeconds(1.2f);

        dialogueRunner.StartDialogue("Outro3");

        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        dialogueRunner.StartDialogue("Outro4");

        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        StartCoroutine(FadeImage(1, fade));

        yield return new WaitForSeconds(1f);

        back2.gameObject.SetActive(false);
        subject2.gameObject.SetActive(false);
        back3.gameObject.SetActive(true);
        subject3.gameObject.SetActive(true);
        fore3.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(FadeImage(-1, fade));

        yield return new WaitForSeconds(1.2f);

        dialogueRunner.StartDialogue("Outro5");

        while (dialogueRunner.IsDialogueRunning)
            yield return null;

        yield return new WaitForSeconds(1f);

        StartCoroutine(FadeImage(1, fade));

        yield return new WaitForSeconds(1.5f);

        if (TempoManager.instance.secret)
        {
            dialogueRunner.StartDialogue("OutroSecret");

            while (dialogueRunner.IsDialogueRunning)
                yield return null;

            yield return new WaitForSeconds(1f);
        }

        //SceneManager.LoadScene("Credits");
    }

    IEnumerator FadeImage(int direction, Image i)
    {
        if (direction > 0)
            while (i.color.a < 1)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime));
                yield return null;
            }
        else
            while (i.color.a > 0)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime));
                yield return null;
            }
    }
}
