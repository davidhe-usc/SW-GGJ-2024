using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioClipCueSO musicCueMainMenu, musicCueMain, musicCuePie, musicCuePouring, musicCueIntro, musicCueEnding, musicCueDialogueBeing, musicCueDialogueEnd;

    private AudioPlayCue musicPlayer;
    [SerializeField] private AudioPlayCue drumRoll;
    [SerializeField] private AudioPlayOneShot drumRollStart;

    private bool canStartDrumRoll = true;

    //tempo
    private float pitchSlideDuration = 1f;
    private AudioSource musicSource;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);

        musicPlayer = GetComponent<AudioPlayCue>();
        musicSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        musicPlayer.Play(false);
    }

    public void ChangeMusic(AudioClipCueSO musicCue)
    {
        if (instance != null)
            StartCoroutine(ChangeMusicRoutine(musicCue));
    }

    private IEnumerator ChangeMusicRoutine(AudioClipCueSO musicCue)
    {
        musicPlayer.Stop(true);
        yield return new WaitForSeconds(musicPlayer.cue.fadeOutTime + 0.5f);
        musicPlayer.cue = musicCue;
        musicPlayer.Play(true);
    }

    public void AnswerSelected()
    {
        SFXOneShots.instance.PlayOneShot(musicCueDialogueEnd);
        StopDrumRoll();
    }

    public void StartDrumRoll()
    {
        if(!canStartDrumRoll) { return; }

        drumRollStart.Play();
        drumRoll.Play(true);
        canStartDrumRoll = false;
    }

    public void StopDrumRoll()
    {
        drumRoll.Stop(true);
        canStartDrumRoll = true;
    }

    public void ChangeAudioTempo(float tempoIncrement)
    {
        AmbienceManager.instance.CheckAmbienceIntensity((int)TempoManager.tempo);

        int tempoDivider = 100;
        float pitchIncrement = tempoIncrement / tempoDivider;
        StartCoroutine(AudioUtility.AudioSourcePitchSlide(musicSource, pitchSlideDuration, pitchIncrement));

    }

    public void ResetTempo()
    {
        float pitchDifference = Mathf.Abs(1 - musicSource.pitch);
        ChangeAudioTempo(-pitchDifference);

        AmbienceManager.instance.CheckAmbienceIntensity((int)TempoManager.tempo);
        print("RESET TEMPO");
    }
}