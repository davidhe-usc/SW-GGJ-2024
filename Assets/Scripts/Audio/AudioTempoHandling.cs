using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTempoHandling : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    private float pitchSlideDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
            ChangePitch(0.1f);
        if (Input.GetKey(KeyCode.KeypadMinus))
            ChangePitch(-0.1f);
        if (Input.GetKey(KeyCode.KeypadEnter))
            ResetPitch();
    }

    public void ChangePitch(float pitchIncrement)
    {
        StartCoroutine(AudioUtility.AudioSourcePitchSlide(musicSource, pitchSlideDuration, pitchIncrement));
    }

    public void ResetPitch()
    {
        float pitchDifference = Mathf.Abs(1 - musicSource.pitch);
        ChangePitch(-pitchDifference);
    }
}
