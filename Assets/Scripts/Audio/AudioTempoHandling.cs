using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTempoHandling : MonoBehaviour
{
    public static AudioTempoHandling instance;

    [SerializeField] private AudioSource musicSource;

    private float pitchSlideDuration = 1f;

    void Awake()
    {
        instance = this;
    }
 
    public void ChangeAudioTempo(float tempoIncrement)
    {
        int tempoDivider = 90;
        float pitchIncrement = tempoIncrement / tempoDivider;
        StartCoroutine(AudioUtility.AudioSourcePitchSlide(musicSource, pitchSlideDuration, pitchIncrement));

        AmbienceManager.instance.CheckAmbienceIntensity((int)TempoManager.tempo);
    }

    public void ResetPitch()
    {
        float pitchDifference = Mathf.Abs(1 - musicSource.pitch);
        ChangeAudioTempo(-pitchDifference);
    }
}
