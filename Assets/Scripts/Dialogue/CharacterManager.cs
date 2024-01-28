using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CharacterManager : MonoBehaviour
{
    Animator characterAnimator;
    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    [YarnCommand("changeExpression")]
    public void UpdateCharacterExpression(string newExpression)
    {
        Debug.Log("New Expression: " + newExpression);
        characterAnimator.SetTrigger(newExpression);

        BackgroundMoodSwitch bg = FindObjectOfType<BackgroundMoodSwitch>();

        if (newExpression == "Disgusted")
        {
            bg.BackgroundDisgusted();
        }

        else
        {
            bg.BackgroundRestore();
        }
    }
}
