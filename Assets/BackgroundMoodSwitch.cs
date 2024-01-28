using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoodSwitch : MonoBehaviour
{
    [SerializeField]
    Sprite[] backgrounds;
    SpriteRenderer backgroundRenderer;
    // Start is called before the first frame update
    void Start()
    {
        backgroundRenderer = GetComponent<SpriteRenderer>();
    }

    public void BackgroundDisgusted()
    {
        backgroundRenderer.sprite = backgrounds[1];
    }

    public void BackgroundRestore()
    {
        backgroundRenderer.sprite = backgrounds[0];
    }
}
