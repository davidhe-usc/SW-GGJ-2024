using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogSlider : MonoBehaviour
{
    private Slider dateSlider;
    [SerializeField]
    private PouringGame gameController;
    [SerializeField]
    private Animator dogAnimator;

    private bool freeze = false;

    void Start()
    {
        dateSlider = gameObject.GetComponent<Slider>();
        dateSlider.value = 0;
    }

    void Update()
    {
        if (freeze)
        {
            return;
        }
        dateSlider.value = 1.0f * gameController.ballsInTarget  / gameController.dogVersionGoal;
        if (dateSlider.value == 1.0f) {
            freeze = true;
        }

        if (dateSlider.value >= 0.5f)
        {
            dogAnimator.SetBool("Happy", true);
        }
        else
        {
            dogAnimator.SetBool("Happy", false);
        }

    }
}
