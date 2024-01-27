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
        AudioTempoHandling[] tms = FindObjectsOfType<AudioTempoHandling>();

        if (tms.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ChangeAudioTempo(float tempoIncrement)
    {
        int tempoDivider = 25;
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
