using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioClipCueSO musicCueMainMenu, musicCueDate, musicCuePie, musicCuePouring;

    private AudioPlayCue musicPlayer;

    private void Awake()
    {
        MusicManager[] tms = FindObjectsOfType<MusicManager>();

        if (tms.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    public IEnumerator ChangeMusic(AudioClipCueSO musicCue)
    {
        musicPlayer.Stop(true);
        yield return new WaitForSeconds(musicPlayer.cue.fadeOutTime);
        musicPlayer.cue = musicCue;
        musicPlayer.Play(true);
    }
}