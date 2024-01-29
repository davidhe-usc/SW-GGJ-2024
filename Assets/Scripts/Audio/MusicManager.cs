using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioClipCueSO musicCueMainMenu, musicCueMain, musicCuePie, musicCuePouring, musicCueIntro, musicCueEnding, musicCueDialogueBeing, musicCueDialogueEnd;

    private AudioPlayCue musicPlayer;

    private void Awake()
    {
        //MusicManager[] tms = FindObjectsOfType<MusicManager>();

        //if (tms.Length > 1)
        //{
         //   Destroy(this.gameObject);
        //}

        //DontDestroyOnLoad(this.gameObject);
        instance = this;

        musicPlayer = GetComponent<AudioPlayCue>();
    }

    private void Start()
    {
        //ChangeMusic(musicCueMain);
    }

    public void ChangeMusic(AudioClipCueSO musicCue)
    {
        if(this != null)
            StartCoroutine(ChangeMusicRoutine(musicCue));
    }

    private IEnumerator ChangeMusicRoutine(AudioClipCueSO musicCue)
    {
        musicPlayer.Stop(true);
        yield return new WaitForSeconds(musicPlayer.cue.fadeOutTime);
        musicPlayer.cue = musicCue;
        musicPlayer.Play(true);
    }
}