using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXOneShots : MonoBehaviour
{
    public static SFXOneShots instance;

    private AudioPlayOneShot audioPlayer;

    public AudioClipCueSO sfxPieThrow, sfxPieLand, sfxPieMiss;
    [Space]
    public AudioClipCueSO sfxMinigameWin;
    public AudioClipCueSO sfxMinigameLose;
    [Space]
    public AudioClipCueSO sfxDialogueGenuine;
    public AudioClipCueSO sfxDialogueWrong, sfxDialogueNeutral, sfxDialogueHonk, sfxDialogueSelect;

    private void Awake()
    {
        //SFXOneShots[] tms = FindObjectsOfType<SFXOneShots>();

       // if (tms.Length > 1)
        //{
        //    Destroy(this.gameObject);
        //}

        //DontDestroyOnLoad(this.gameObject);
        instance = this;

        audioPlayer = GetComponent<AudioPlayOneShot>();
    }

    public void PlayOneShot(AudioClipCueSO audioCue)
    {
        audioPlayer.Play(audioCue);
    }
}
