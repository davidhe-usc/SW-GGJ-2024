using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateSlider : MonoBehaviour
{
    private Slider dateSlider;
    [SerializeField]
    private PouringGame gameController;

    void Start()
    {
        dateSlider = gameObject.GetComponent<Slider>();
        dateSlider.value = 0.5f;
    }

    void Update()
    {
        if (gameController.numBalls > 0) {
            print(gameController.ballsInTarget / gameController.numBalls);
            dateSlider.value = 1.0f * gameController.ballsInTarget  / gameController.numBalls;
        }
    }
}
