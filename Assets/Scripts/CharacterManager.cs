using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    Animator characterAnimator;
    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    void UpdateCharacterExpression(string newExpression)
    {
        characterAnimator.SetTrigger(newExpression);
    }
}
